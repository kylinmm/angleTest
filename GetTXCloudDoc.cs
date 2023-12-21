using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace angleTest
{
    public  class GetTXCloudDoc
    {
        public async Task<bool> getTXDocWeb() {

            var client = new HttpClient();
            var responseBody = await client.GetStringAsync("https://docs.qq.com/sheet/DTmJ0SVdMU0NEUnBE?tab=9x16sg");

            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(responseBody);

            var table=document.QuerySelector("table");//解析获取table所有数据
            Console.WriteLine($"Serializing the (original) document:{table.OuterHtml}");

            var head = table.QuerySelector("thead");
            var transverseTitle = head.QuerySelectorAll("th");//获取execl横向标题
            DataTable outTable = new DataTable();
            bool flag = true;
            foreach (var item in transverseTitle)
            {
                if (flag)
                {
                    outTable.Columns.Add("坐标", typeof(String));
                    flag = false;
                }
                else {
                    outTable.Columns.Add(item.Text(), typeof(String));
                }
            }

           
            var body = table.QuerySelector("tbody");
            var rows = body.QuerySelectorAll("tr");
           
            foreach (var row in rows)
            {
                var cells = row.QuerySelectorAll("th,td");//获取表格每行数据
                DataRow dataRow = outTable.NewRow();
                for (int j = 0; j < cells.Length; j++)
                {
                    if (string.IsNullOrEmpty(cells[j].Text()))
                    {
                        dataRow[j] = cells[j].QuerySelector("span")?.Text();
                    }
                    else
                    {
                        dataRow[j] = cells[j].Text();
                    }

                }
                outTable.Rows.Add(dataRow);
              
            }


            return true;

        }
    }


}
