using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotNet4.Utilities;
using Ivony.Html;
using Ivony.Html.Parser;

namespace zhenyoumei
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpHelper http = new HttpHelper();
            int max = 1045;
            for (int i = 2; i <= max; i++)
            {
                Console.Write("开始下载...\n");

                string baseUrl = "http://www.zhenyoumei.com/anli/" + i + ".html";
                HttpItem item = new HttpItem()
                {
                    URL = baseUrl, //URL     必需项
                    Encoding = null, //编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    //Encoding = Encoding.Default,
                    Method = "get", //URL     可选项 默认为Get
                    Timeout = 100000, //连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000, //写入Post数据超时时间     可选项默认为30000
                    IsToLower = false, //得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "", //字符串Cookie     可选项
                    UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                    //用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*", //    可选项有默认值
                    ContentType = "text/html", //返回类型    可选项有默认值
                    Referer = "http://www.zhenyoumei.com", //来源URL     可选项
                    Connectionlimit = 1024, //最大连接数     可选项 默认为1024
                    PostDataType = PostDataType.FilePath, //默认为传入String类型，也可以设置PostDataType.Byte传入Byte类型数据
                    ResultType = ResultType.Byte, //返回数据类型，是Byte还是String

                    CookieCollection = new System.Net.CookieCollection(), //可以直接传一个Cookie集合进来
                };
                //item.Header.Add("测试Key1", "测试Value1");
                //item.Header.Add("测试Key2", "测试Value2");
                //得到HTML代码

                Console.Write(baseUrl + "\n");
                HttpResult result = http.GetHtml(item);

                SetCasses(result);
                //表示StatusCode的文字说明与描述
                string statusCodeDescription = result.StatusDescription;
                //把得到的Byte转成图片
                // MediaTypeNames.Image img = byteArrayToImage(result.ResultByte);
            }
            if (max == 1045)
            {
                Console.Write("下载完成");
            }

        }



        public static void SetCasses(HttpResult result)
        {
            //取出返回的Cookie
            string cookie = result.Cookie;
            //返回的Html内容
            string html = result.Html;
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类

                //var resultHtml=  StripHTML(html);
                var resultHtml = html;
                JumonyParser jp = new JumonyParser();
                var data = jp.Parse(resultHtml);
                Cases obj = new Cases();
                obj.CaseId = Guid.NewGuid();
                obj.Title = data.Find(".flleft>h1.riji_bt").Single().InnerText();
                obj.TagName = data.Find(".biaoqian_all span a").Single().InnerText();
                obj.CreateTime = DateTime.Parse(data.Find("ul.ll_zan>li.time_zx").Single().InnerText());
                //案例内容
                obj.Content = data.Find("div.riji_wenzi").Single().InnerHtml();
                obj.ProjectName = data.Find(".riji_fy>h2").Single().InnerText();
                obj.Price = data.Find(".riji_fy>p>span").Single().InnerText();
                var itempage = data.Find(".riji_fy>ul.biapge_list>li").ToList();
                for (int i = 0; i < itempage.Count; i++)
                {
                    obj.TreatmentWay = itempage[0].Find("span.listspan_02").Single().InnerText();
                    obj.ResultData = itempage[1].Find("span.listspan_02").Single().InnerText();
                    obj.Durations = itempage[2].Find("span.listspan_02").Single().InnerText();
                    obj.ResultSpeed = itempage[3].Find("span.listspan_02").Single().InnerText();
                    obj.ApplicableCrowd = itempage[4].Find("span.listspan_02").Single().InnerText();
                }
              
              var  fileup= data.Find("div.riji_wenzi>p>img").ToList();
                foreach (var item in fileup)
                {

                
                       var surl= item.Attribute("src").Value();
                        FileDown(surl);
                 
                }
                if (AddCase(obj)==1)
                {
                    Console.Write("Downing ok!\n");
                }








            }
        }

        public static int AddCase(Cases obj)
        {
            string sql = "insert into [Cases]([Title],[TagName],[CreateTime],[Content],[ProjectName],[TreatmentWay],[ResultData],[Durations],[ResultSpeed],[ApplicableCrowd],[Price],[SourceDevice]) values " +
                         "(@Title,@TagName,@CreateTime,@Content,@ProjectName,@TreatmentWay,@ResultData,@Durations,@ResultSpeed,@ApplicableCrowd,@Price,@SourceDevice)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Title",obj.Title), 
                new SqlParameter("@TagName",obj.TagName), 
                new SqlParameter("@CreateTime",obj.CreateTime), 
                new SqlParameter("@Content",obj.Content), 
                new SqlParameter("@ProjectName",obj.ProjectName), 
                new SqlParameter("@TreatmentWay",obj.TreatmentWay), 
                new SqlParameter("@ResultData",obj.ResultData), 
                new SqlParameter("@Durations",obj.Durations), 
                new SqlParameter("@ResultSpeed",obj.ResultSpeed), 
                new SqlParameter("@ApplicableCrowd",obj.ApplicableCrowd), 
                new SqlParameter("@Price",obj.Price), 
                new SqlParameter("@SourceDevice","真优美"), 
            };

            int result = 0;


        
            return result;
        }



        public static string GetUrltoHtml(string Url, string type)
        {
            try
            {
                System.Net.WebRequest wReq = System.Net.WebRequest.Create(Url);
                // Get the response instance.
                System.Net.WebResponse wResp = wReq.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                // Dim reader As StreamReader = New StreamReader(respStream)
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                //errorMsg = ex.Message;
            }
            return "";
        }


        public static void FileDown(string URLAddress)
        {
            WebClient client=new WebClient();
            Stream str = client.OpenRead(URLAddress);
            StreamReader reader = new StreamReader(str);
            byte[] mbyte = new byte[1000000];
            int allmybyte = (int)mbyte.Length;
            int startmbyte = 0;

            while (allmybyte > 0)
            {

                int m = str.Read(mbyte, startmbyte, allmybyte);
                if (m == 0)
                    break;

                startmbyte += m;
                allmybyte -= m;
            }

            reader.Dispose();
            str.Dispose();
            Path.GetExtension(URLAddress);
            string path = "..\\..\\Files\\CASEs\\" + System.IO.Path.GetFileName(URLAddress);
            FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fstr.Write(mbyte, 0, startmbyte);
            fstr.Flush();
            fstr.Close();
        }




    }


}
