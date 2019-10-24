using Microsoft.AspNetCore.Http;
using System;
using System.Xml;

namespace Interfactura.Web.UI.Models.Utilities
{
    public static class ProcessXMLFile
    {
        private const string _versionComprobante = "3.3";

        public static string ValidateXML(IFormFile xmlFile, string rfcAlumno)
        {
            string regresa = string.Empty;
            try
            {
                using (var fileStream = xmlFile.OpenReadStream())
                {
                    XmlDocument document = new XmlDocument();
                    document.Load(fileStream);

                    regresa += ValidateEmisor(document, rfcAlumno);
                    regresa += ValidateDocumentVersion(document);
                    regresa += ValidateTotals(document);
                }
            }
            catch (Exception ex) { regresa = $"error: {ex.Message}"; }

            return regresa;
        }

        private static string ValidateTotals(XmlDocument document)
        {
            const string error = "La suma del importe de los conceptos no coincide con el subtotal del documento XML.";
            XmlNodeList nodos = document.GetElementsByTagName("cfdi:Concepto");
            if (nodos is null) return error;

            decimal nodesTotalSum = 0;
            
            foreach (XmlNode nodo in nodos)
            {
                string attrValue = nodo.Attributes.GetNamedItem("Importe").Value;
                if (string.IsNullOrWhiteSpace(attrValue)) continue;

                nodesTotalSum+= GetValue(attrValue);
            }

            decimal SubTotal = GetSubtotal(document);
            if (SubTotal != nodesTotalSum) return error;

            return string.Empty;
        }

        private static decimal GetValue(string attrValue)
        {
            decimal valueToReturn = 0;
            decimal importe = 0 ;

            var values = attrValue.Split(".");
            if (values.Length == 0) return 0;

            bool success = decimal.TryParse(values[0], out importe);
            if (success) valueToReturn += importe;

            if (values.Length == 1) return valueToReturn;
            success = decimal.TryParse(values[1], out importe);
            if (success) valueToReturn += (importe/100);

            return valueToReturn;
        }

        private static decimal GetSubtotal(XmlDocument document)
        {
            XmlNode nodo = document.GetElementsByTagName("cfdi:Comprobante")[0];
            if (nodo is null) return 0;

            var subTotal = nodo.Attributes.GetNamedItem("SubTotal").Value;
            if (string.IsNullOrWhiteSpace(subTotal)) return 0;
            return GetValue(subTotal);
        }

        private static string ValidateDocumentVersion(XmlDocument document)
        {
            const string error = "La versión del documento XML es incorrecta";
            XmlNode nodo = document.GetElementsByTagName("cfdi:Comprobante")[0];
            if (nodo is null) return error;

            var version = nodo.Attributes.GetNamedItem("Version").Value;
            if (!version.Equals(_versionComprobante)) return error;

            return string.Empty;
        }

        private static string ValidateEmisor(XmlDocument document, string rfcAlumno)
        {
            const string error = "El RFC del documento XML no corresponde con el RFC del alumno.";

            XmlNode nodo = document.GetElementsByTagName("cfdi:Emisor")[0];
            if (nodo is null) return error;

            var rfc = nodo.Attributes.GetNamedItem("Rfc").Value;
            if (!rfc.Equals(rfcAlumno)) return error;

            return string.Empty;
        }
    }
}
