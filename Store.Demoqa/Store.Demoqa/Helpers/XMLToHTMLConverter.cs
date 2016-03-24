using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Store.Helpers
{
    public class XMLToHTMLConverter
    {
        public void TranformXMLToHTML()
        {
            XslCompiledTransform transformer = new XslCompiledTransform();
            XmlTextReader reader = new XmlTextReader(Config.SchemaFilePath);
            transformer.Load(reader);
            XPathDocument docToConvert = new XPathDocument(Config.TestResultsXMLFilePath);
            XmlTextWriter writer = new XmlTextWriter(Config.TestResultsHTMLFilePath, null);
            transformer.Transform(docToConvert, null, writer);
        }
    }
}
