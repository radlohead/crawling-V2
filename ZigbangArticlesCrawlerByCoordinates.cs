using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Sugarhill.OfficeSquare.WebJob.Zigbang
{
    public class ZigbangArticlesCrawlerByCoordinates
    {
        public static void StartUp()
        {
            Process();
        }

        private static void Process()
        {
            double longitudeOffset = 0.016;
            double latitudeOffset = 0.047;

            //double maxNorthLatitude = 38.612607;
            //double maxSouthLatitude = 34.287754;
            //double maxEastLongitude = 129.582132;
            //double maxWestLongitude = 126.090724;

            // JEJU
            double maxNorthLatitude = 33.56684;
            double maxSouthLatitude = 33.194724;
            double maxEastLongitude = 126.971250;
            double maxWestLongitude = 126.161033;

            int itemNum = 1;

            for (var longitude = maxWestLongitude; longitude <= maxEastLongitude; longitude += longitudeOffset)
            {
                for (var latitude = maxSouthLatitude; latitude <= maxNorthLatitude; latitude += latitudeOffset)
                {
                    var leftLon = longitude;
                    var rightLon = longitude + longitudeOffset;
                    var topLat = latitude + latitudeOffset;
                    var bottomLat = latitude;

                    //Thread.Sleep(200);

                    var models = GetModels(leftLon, rightLon, topLat, bottomLat);

                    Console.WriteLine(models.total_count);

                    if (models.list_items.Length > 0)
                    {
                        
                        int itemSize = 100;
                        int[][] arr = new int[models.list_items.Length][];
                        int j = 0;
                        int i = 0;

                        for (var k = 0; k < models.list_items.Length; k++)
                        {
                            arr[k] = new int[itemSize];
                            arr[j][i] = models.list_items[k].simple_item.item_id;
                            ++i;

                            if (i == itemSize)
                            {
                                using (StreamWriter file = File.AppendText($"itemId/zigbang-itemId_{itemNum}.json"))
                                {
                                    file.WriteLine(JsonConvert.SerializeObject(arr[j]));
                                    file.Flush();
                                }

                                long length = new System.IO.FileInfo($"itemId/zigbang-itemId_{itemNum}.json").Length;

                                if (length > 168000)
                                {
                                    ++itemNum;
                                }

                                ++j;
                                i = 0;
                            }
                        }
                    }

                    Console.WriteLine($"The articles in inserted. leftLon={leftLon}, rightLon={rightLon}, topLat{topLat}, bottomLat={bottomLat}");
                }
            }
        }

        private static ArticlesModel GetModels(double leftLon, double rightLon, double topLat, double bottomLat)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://apis.zigbang.com/v3/items2";

                Uri uri = new Uri($"{url}?room=[01,02,03,04,05]" +
                    //$"&lng_east={127.01088889405688}" +
                    //$"&lng_west={126.74530304457262}" +
                    //$"&lat_north={37.72180389507605}" +
                    //$"&lat_south={37.47905525641133}");

                    $"&lng_east={rightLon}" +
                    $"&lng_west={leftLon}" +
                    $"&lat_north={topLat}" +
                    $"&lat_south={bottomLat}");

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header

                var response = client.GetAsync(uri).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    var model = JsonConvert.DeserializeObject<ArticlesModel>(json);

                    if (model == null)
                    {
                        return null;
                    }

                    try
                    {
                        return model;
                    }
                    catch
                    {
                        return null;
                    }
                }

                throw new Exception("Api에 연결할 수 없습니다.");
            }
        }
    }

    public class ArticlesModel
    {
        public List_Items[] list_items { get; set; }
        public int total_count { get; set; }
        public Section[] sections { get; set; }
    }

    public class List_Items
    {
        public Simple_Item simple_item { get; set; }
        public string section_title { get; set; }
        public string section_type { get; set; }
        public int position { get; set; }
    }

    public class Simple_Item
    {
        public int item_id { get; set; }
    }

    public class Section
    {
        public string section_title_color { get; set; }
        public string section_type { get; set; }
        public int section_item_count { get; set; }
        public string section_title { get; set; }
        public string section_icon { get; set; }
        public string section_bg_color { get; set; }
        public string event_button_icon { get; set; }
    }
}
