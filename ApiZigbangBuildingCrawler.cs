using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;

namespace Sugarhill.OfficeSquare.WebJob.Zigbang
{
    public class ApiZigbangBuildingCrawler
    {
        public class Item2
        {
            public int item_id { get; set; }
        }

        public static void AgentList(RootModel models, int itemNum)
        {
            if (models == null)
            {
                return;
            }

            if (models.items.Length > 0)
            {
                using (StreamWriter file2 = File.AppendText($"agentInfo/zigbang-agentInfo_{itemNum}.json"))
                {
                    List<ItemAgent> data = new List<ItemAgent>();

                    Item[] items = models.items;

                    if (models.count_agent < 98)
                    {
                        return;
                    }

                    for (var i = 0; i < models.count_agent; i++)
                    {
                        data.Add(new ItemAgent()
                        {
                            user_no = items[i].item.user_no,
                            agent_name = items[i].item.agent_name,
                            agent_phone = items[i].item.agent_phone,
                            agent_mobile = items[i].item.agent_mobile,
                            agent_email = items[i].item.agent_email,
                            agent_local1 = items[i].item.agent_local1,
                            agent_local2 = items[i].item.agent_local2,
                            agent_address1 = items[i].item.agent_address1
                        });
                    }

                    string json = JsonConvert.SerializeObject(data);

                    file2.WriteLine(json);
                    file2.Flush();
                }
            }
        }

        public static void StartUp(int itemNum)
        {
            using (StreamReader file = new StreamReader($"itemId/zigbang-itemId_{itemNum}.json"))
            {
                string line = "";

                while ((line = file.ReadLine()) != null)
                {
                    var models = GetModel(line);
                    AgentList(models, itemNum);
                }
            }
        }

        public static RootModel GetModel(string itemId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(2);

                string url = "https://apis.zigbang.com/v3/items";

                Uri uri = new Uri($"{url}?detail=true&item_ids={itemId}");

                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json")); //ACCEPT header

                try
                {
                    var response = client.GetAsync(uri).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        var model = JsonConvert.DeserializeObject<RootModel>(json);

                        return model;
                    }
                }
                catch
                {
                    return null;
                }

                throw new Exception("Api에 연결할 수 없습니다.");
            }
        }
    }


    public class RootModel
    {
        public Item[] items { get; set; }
        public int count_agent { get; set; }
        public int count_direct { get; set; }
        public string korea_local_time { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public bool header { get; set; }
        public int header_height { get; set; }
        public Item1 item { get; set; }
    }

    public class ItemAgent
    {
        public int user_no { get; set; }
        public string agent_name { get; set; }
        public string agent_phone { get; set; }
        public string agent_mobile { get; set; }
        public string agent_email { get; set; }
        public string agent_local1 { get; set; }
        public string agent_local2 { get; set; }
        public string agent_address1 { get; set; }
    }

    public class Item1
    {
        public int id { get; set; }
        public Image[] images { get; set; }
        public bool is_realestate { get; set; }
        public bool is_direct { get; set; }
        public bool is_room { get; set; }
        public bool is_type_room { get; set; }
        public int rent { get; set; }
        public int deposit { get; set; }
        public bool is_deposit_only { get; set; }
        public string floor { get; set; }
        public string _floor { get; set; }
        public string floor_all { get; set; }
        public string local1 { get; set; }
        public string local2 { get; set; }
        public string local3 { get; set; }
        public string title { get; set; }
        public string room_type { get; set; }
        public string room_type_code { get; set; }
        public string building_type { get; set; }
        public string room_gubun_code { get; set; }
        public string status { get; set; }
        public bool is_status_open { get; set; }
        public bool is_status_close { get; set; }
        public int view_count { get; set; }
        public string updated_at { get; set; }
        public string updated_at2 { get; set; }
        public string read_updated_at { get; set; }
        public float size_m2 { get; set; }
        public float size { get; set; }
        public object size_m2_contract { get; set; }
        public float size_contract { get; set; }
        public string address1 { get; set; }
        public string near_subways { get; set; }
        public string random_location { get; set; }
        public string options { get; set; }
        public string manage_cost { get; set; }
        public string manage_cost_inc { get; set; }
        public string parking { get; set; }
        public string elevator { get; set; }
        public string movein_date { get; set; }
        public string description { get; set; }
        public string description_og { get; set; }
        public string pets_text { get; set; }
        public string loan_text { get; set; }
        public string room_direction_text { get; set; }
        public bool is_owner { get; set; }
        public string images_thumbnail { get; set; }
        public bool is_zzim { get; set; }
        public object secret_memo { get; set; }
        public int user_no { get; set; }
        public bool user_has_penalty { get; set; }
        public bool user_has_no_penalty { get; set; }
        public string user_name { get; set; }
        public string user_phone { get; set; }
        public string original_user_phone { get; set; }
        public string user_mobile { get; set; }
        public string user_email { get; set; }
        public string user_intro { get; set; }
        public string agent_name { get; set; }
        public int agent_no { get; set; }
        public string agent_phone { get; set; }
        public string agent_mobile { get; set; }
        public string agent_email { get; set; }
        public string agent_local1 { get; set; }
        public string agent_local2 { get; set; }
        public string agent_address1 { get; set; }
        public float agent_lng { get; set; }
        public float agent_lat { get; set; }
        public Building building { get; set; }
        public string contract { get; set; }
        public string bjd_code { get; set; }
        public string bonbun_code { get; set; }
        public string bubun_code { get; set; }
        public string address2 { get; set; }
        public object address3 { get; set; }
        public bool is_premium { get; set; }
        public bool is_premium2 { get; set; }
        public bool is_homepage { get; set; }
        public string profile_url { get; set; }
        public string agent_comment { get; set; }
        public object[] premium_items { get; set; }
        public string pnu { get; set; }
    }

    public class Building
    {
        public int building_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string local1 { get; set; }
        public string local2 { get; set; }
        public string local3 { get; set; }
        public string address2 { get; set; }
        public string floor { get; set; }
        public string rooms { get; set; }
        public string elevator { get; set; }
        public int count { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }
        public string established { get; set; }
    }

    public class Image
    {
        public int index { get; set; }
        public int count { get; set; }
        public string url { get; set; }
    }
}
