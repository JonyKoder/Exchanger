using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class Prog
    {

        static void Main(string[] args)
        {
            WebClient client = new ();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            client.Encoding = Encoding.GetEncoding(1251);
            string reply = client.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp");
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
            ValCurs curs = null;
            using (StringReader reader = new StringReader(reply))
            {
                curs = (ValCurs)serializer.Deserialize(reader);
            }
            var crona = curs.Valute.SingleOrDefault(x => x.NumCode == 578);
            var cronValue = double.Parse(crona.Value) / crona.Nominal;
            var forint = curs.Valute.SingleOrDefault(x => x.NumCode == 348);
            var forintValue = double.Parse(forint.Value) / forint.Nominal;
            var result = cronValue / forintValue;
            Console.WriteLine($"1 Норвежская крона стоит {Math.Round(result, 2)} {forint.Name}");
            

           

        }
    }
}
   
