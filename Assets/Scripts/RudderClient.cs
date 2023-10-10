//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using RudderStack;

//public class RudderClient : MonoBehaviour
//{

//  //  RudderClient.SerializeSqlite();

//    List<User> ConnectToDB()
//    {
//        List<User> users = new List<User>();

//        // Build Config 

//        RudderConfigBuilder configBuilder = new RudderConfigBuilder()
//            .WithDataPlaneUrl(DATA_PLANE_URL);
//            .WithLogLevel(RudderLogLevel.VERBOSE)

//        // Build connection string
//        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
//        builder.DataSource = "<sql server address>";
//        builder.UserID = "<login>";
//        builder.Password = "<password>";
//        builder.InitialCatalog = "<databases>";

//        try
//        {
//            // connect to the databases
//            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
//            {
//                // if open then the connection is established
//                connection.Open();
//                Debug.Log("connection established");
//                // sql command
//                string sql = "";
//                // execute sql command
//                using (SqlCommand command = new SqlCommand(sql, connection))
//                {
//                    // read
//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                        // each line in the output
//                        while (reader.Read())
//                        {
//                            // to avoid SqlNullValueException
//                            if (!reader.IsDBNull(0)
//                                                   & !reader.IsDBNull(1)
//                                                   & !reader.IsDBNull(3))
//                            {
//                                // Skills list to be attached to each user object
//                                List<Skill> skills = new List<Skill>();
//                                // get output parameters
//                                string username = reader.GetString(0);
//                                string aboutString = reader.GetString(1);
//                                string skillsString = reader.GetString(3);
//                                // as we are getting a list of skills as 
//                                // a single string delimited by comma
//                                // we split the string
//                                string[] skillsList = skillsString.Split(',');
//                                // we now iterate through each skill to initialize our
//                                // skill object and put it into skills list
//                                foreach (string skillName in skillsList)
//                                {
//                                    // initialize a skill object with a trimmed string
//                                    Skill skill = new Skill(skillName.Trim());
//                                    // append to the skills array
//                                    skills.Add(skill);
//                                }
//                                // initialize User oobject
//                                User user = new User(username.Trim(), aboutString.Trim(), skills);
//                                users.Add(user);
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        catch (SqlException e)
//        {
//            Debug.Log(e.ToString());
//        }
//        return users;
//    }


//    public void Identify()
//    {
//        RudderMessage identifyMessage = new RudderMessageBuilder().Build();
//        RudderTraits traits = new RudderTraits();
//        //pre-defined API's for inserting standard traits
//        traits.PutEmail("abc@example.com");
//        traits.PutAge("38");
//        //Put API to insert custom traits
//        traits.Put("location", "Mysuru");
//        traits.Put("gender", "Male");
//        traits.Put("consent", "Granted");
//        rudderClient.Identify("some_user_id", traits, identifyMessage);

//    }
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
