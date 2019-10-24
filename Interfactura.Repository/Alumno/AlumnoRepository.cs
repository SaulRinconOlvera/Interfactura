using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Interfactura.ViewModel;

namespace Interfactura.Repository.Alumno
{
    public class AlumnoRepository : IAlumnoRepository
    {
        public void Add(AlumnoViewModel viewModel)
        {
            string fileName = $"{Directory.GetCurrentDirectory()}\\Alumnos\\{viewModel.RFC}.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(viewModel.GetType());
            using (TextWriter txtWriter = new StreamWriter(fileName, false, Encoding.UTF8))
                xmlSerializer.Serialize (txtWriter, viewModel);
        }
    }
}
