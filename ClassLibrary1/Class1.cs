using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public class CurrencyRate
    {
        /// <summary>
        /// Закодированное строковое обозначение валюты
        /// Например: USD, EUR, AUD и т.д.
        /// </summary>
        public string CurrencyStringCode;

        /// <summary>
        /// Наименование валюты
        /// Например: Доллар, Евро и т.д.
        /// </summary>
        public string CurrencyName;

        /// <summary>
        /// Обменный курс
        /// </summary>
        public double ExchangeRate;
    }

    public class CurrencyRates
    {
        public class ValCurs
        {
            [XmlElementAttribute("Valute")]
            public ValCursValute[] ValuteList;
        }

        public class ValCursValute
        {

            [XmlElementAttribute("CharCode")]
            public string ValuteStringCode;

            [XmlElementAttribute("Name")]
            public string ValuteName;

            [XmlElementAttribute("Value")]
            public string ExchangeRate;
        }

        /// <summary>
        /// Получить список котировок ЦБ ФР на данный момент
        /// </summary>
        /// <returns>список котировок ЦБ РФ</returns>
        public static List<CurrencyRate> GetExchangeRates()
        {
            List<CurrencyRate> result = new List<CurrencyRate>();
            XmlSerializer xs = new XmlSerializer(typeof(ValCurs));
            XmlReader xr = new XmlTextReader(@"http://www.cbr.ru/scripts/XML_daily.asp");
            foreach (ValCursValute valute in ((ValCurs)xs.Deserialize(xr)).ValuteList)
            {
                result.Add(new CurrencyRate()
                {
                    CurrencyName = valute.ValuteName,
                    CurrencyStringCode = valute.ValuteStringCode,
                    ExchangeRate = Convert.ToDouble(valute.ExchangeRate)
                });
            }
            return result;
        }
    }
}

