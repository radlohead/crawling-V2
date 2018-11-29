using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sugarhill.OfficeSquare.WebJob.Zigbang
{
    public class DuplicateRemove
    {
        public static void hashSet()
        {
            HashSet<int> user_no = new HashSet<int>();

            //int result = 0;

            using (StreamReader file = new StreamReader($"agentInfo/zigbang-duplicateRemove_total.json"))
            {
                string line = "";

                //line = file.ReadToEnd();

                //line = line.Replace("[", "").Replace("}]", "},");

                //using (StreamWriter file2 = new StreamWriter(@"agentInfo/zigbang-final.json"))
                //{
                //    file2.Write(line);
                //    file2.Flush();
                //}

                //using (StreamWriter file2 = new StreamWriter(@"agentInfo/zigbang-final.json"))
                //{
                //    MemoryStream stream1 = new MemoryStream();
                //    file2.Write(JsonConvert.DeserializeObject<List<RootobjectIAgent>>(line));
                //    file2.Flush();
                //}

                while ((line = file.ReadLine()) != null)
                {
                    if (line == null)
                    {
                        return;
                    }

                    //var item = JsonConvert.DeserializeObject<List<RootobjectIAgent>>(line);

                    //result += item.Count;

                    if (line.Length > 0)
                    {
                        var item = JsonConvert.DeserializeObject<List<RootobjectIAgent>>(line);

                        List<RootobjectIAgent> data = new List<RootobjectIAgent>();
                        RootobjectItem[] items = new RootobjectItem[] { };

                        for (int i = 0; i < 98; i++)
                        {
                            if (user_no.Add(item[i].user_no))
                            {
                                data.Add(new RootobjectIAgent()
                                {
                                    user_no = item[i].user_no,
                                    agent_name = item[i].agent_name,
                                    agent_phone = item[i].agent_phone,
                                    agent_mobile = item[i].agent_mobile,
                                    agent_email = item[i].agent_email,
                                    agent_local1 = item[i].agent_local1,
                                    agent_local2 = item[i].agent_local2,
                                    agent_address1 = item[i].agent_address1
                                });
                            }

                            if (i % 97 == 0 && i > 0)
                            {
                                using (StreamWriter file2 = File.AppendText($"agentInfo/zigbang-duplicateRemove_total.json"))
                                {
                                    string json = JsonConvert.SerializeObject(data);

                                    if (json.Length > 97)
                                    {
                                        file2.WriteLine(json);
                                        file2.Flush();
                                    }
                                }
                            }
                        }
                    }
                }

                //Console.WriteLine(result);
                //Console.ReadKey();
            }

            Console.Write("evenNumbers contains {0} elements: ", user_no.Count);
            DisplaySet(user_no);
        }

        private static void DisplaySet(HashSet<int> set)
        {
            Console.Write("{");
            foreach (int i in set)
            {
                Console.Write(" {0}", i);
            }
            Console.WriteLine(" }");
        }
    }


    public class Rootobject
    {
        public RootobjectItem[] items { get; set; }
        public int count_agent { get; set; }
        public int count_direct { get; set; }
        public string korea_local_time { get; set; }
    }

    public class RootobjectItem
    {
        public string title { get; set; }
        public bool header { get; set; }
        public int header_height { get; set; }
        public RootobjectItem1 item { get; set; }
    }

    public class RootobjectIAgent
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

    public class RootobjectItem1
    {
        public int id { get; set; }
        public RootobjectImage[] images { get; set; }
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
        public RootobjectBuilding building { get; set; }
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

    public class RootobjectBuilding
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

    public class RootobjectImage
    {
        public int index { get; set; }
        public int count { get; set; }
        public string url { get; set; }
    }
}
