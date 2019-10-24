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

            double nodesTotalSum = 0;
            foreach (XmlNode nodo in nodos)
            {
                var attrValue = nodo.Attributes.GetNamedItem("Importe").Value;
                if (Int64.TryParse(attrValue, out long importe))
                    nodesTotalSum += importe;
            }

            double SubTotal = GetSubtotal(document);
            if (SubTotal != nodesTotalSum) return error;

            return string.Empty;
        }

        private static double GetSubtotal(XmlDocument document)
        {
            XmlNode nodo = document.GetElementsByTagName("cfdi:Comprobante")[0];
            if (nodo is null) return 0;

            var subTotal = nodo.Attributes.GetNamedItem("SubTotal").Value;
            if (string.IsNullOrWhiteSpace(subTotal)) return 0;

            if (Int64.TryParse(subTotal, out long subtotalValue))
                return subtotalValue;
            else return 0;
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
