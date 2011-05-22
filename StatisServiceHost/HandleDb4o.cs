using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using StatisServiceContracts;

namespace StatisServiceHost
{
    public class HandleDb4o
    {
        readonly static string StoreYapFileName = Path.Combine(@"D:\", "store.yap");
            //Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "store.yap");

        private static IObjectContainer GetDb()
        {
            return Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), StoreYapFileName);
        }


        /// <summary>
        /// Ensure we return a copy of the result from DB40
        /// 
        /// DB40 does not have a distinct implementation by default
        /// To stop it returning the categories assigned to the basket products
        /// Create an equality comparison on the Category name
        /// </summary>
        /// <returns></returns>
        public static Questionnaire GetQuestionnaire(string questionnaireName)
        {
            var db = GetDb();
            try
            {
                var questionnaire =
                (from Questionnaire q in db
                 where q.Name == questionnaireName
                 select q).FirstOrDefault();

                return questionnaire;
            }
            finally
            {
                db.Close();
            }
        }

        public static void StoreQuestionnaire(Questionnaire questionnaireToStore)
        {
            var db = GetDb();
            try
            {
                var questionnaire =
                (from Questionnaire q in db
                 where q.Name == questionnaireToStore.Name
                 select q).FirstOrDefault();

                if(questionnaire != null)
                {
                    questionnaire.Description = questionnaireToStore.Description;
                    questionnaire.Name = questionnaireToStore.Name;
                    questionnaire.Questions.Clear();
                    questionnaire.Questions.AddRange(questionnaireToStore.Questions);
                    db.Store(questionnaire);
                }
                else
                {
                    db.Store(questionnaireToStore);
                }
                db.Commit();
                
            }
            finally
            {
                db.Close();
            }
        }

        public static void LoadTestData()
        {
            //File.Delete(StoreYapFileName);

            //IObjectContainer db = GetDB();
            //try
            //{
            //    Category category = new Category { CategoryId = 1, Name = "Bikes" };
            //    StoreCategory(db, category);

            //    Product product = new Product { ProductId = 1, Category = category, Name = "Concorde Bikes PDM Team Replica Road Bike", Price = 699.99M, Url = "/Content/images/concorde-bike-pdm-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 2, Category = category, Name = "Kestrel Talon Tri 105 2010", Price = 147.99M, Url = "/Content/images/kestral-talon-tri-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 3, Category = category, Name = "Felt Brougham 2010", Price = 299.99M, Url = "/Content/images/felt-brougham-2010-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 4, Category = category, Name = "Focus Black Forest Pro 2010 ", Price = 899.99M, Url = "/Content/images/focus-blk-for-pro-2010-ind.jpg" };
            //    StoreProduct(db, product);

            //    category = new Category { CategoryId = 2, Name = "Components" };
            //    StoreCategory(db, category);
            //    product = new Product { ProductId = 5, Category = category, Name = "Look Keo 2 Max Carbon Pedals", Price = 1119.99M, Url = "/Content/images/look-keo2max-c-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 6, Category = category, Name = "Mavic Cosmic Carbone Ultimate Tub Road Wheelset", Price = 2025.00M, Url = "/Content/images/mavic-107529-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 7, Category = category, Name = "Shimano Dyna-Sys Deore XT HollowTech II Triple Chainset", Price = 170.99M, Url = "/Content/images/shimano-dyna-xt-cset-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 8, Category = category, Name = "Continental Grand Prix 4 Season Road Tyre and Tube Set", Price = 71.89M, Url = "/Content/images/tyres.jpg" };
            //    StoreProduct(db, product);

            //    category = new Category { CategoryId = 3, Name = "Clothing" };
            //    StoreCategory(db, category);

            //    product = new Product { ProductId = 9, Category = category, Name = "Rudy Project Exowind Impact X Sunglasses - Photo Lenses", Price = 105.29M, Url = "/Content/images/rudy-exowind-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 10, Category = category, Name = "Bell Team Saxo Bank Volt Road Cycling Helmet", Price = 134.99M, Url = "/Content/images/bell-saxovolt-10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 11, Category = category, Name = "Vermarc Milram Team Short Zip Jersey 2010", Price = 37.50M, Url = "/Content/images/vermarc-milram-jer-10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 12, Category = category, Name = "Nalini Liquigas Team Jersey 2010", Price = 35.24M, Url = "/Content/images/nalini-liquigas-jer-2010-ind.jpg" };
            //    StoreProduct(db, product);

            //    category = new Category { CategoryId = 4, Name = "Shoes" };
            //    StoreCategory(db, category);

            //    product = new Product { ProductId = 13, Category = category, Name = "Asics Ladies Gel Nimbus 12 Shoes", Price = 94.50M, Url = "/Content/images/asics-T095N-0197-ss10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 14, Category = category, Name = "Asics DS Trainer 15 Shoes", Price = 80.10M, Url = "/Content/images/asics-T014N-0191-ss10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 15, Category = category, Name = "Salomon XA Pro 3D Ultra GTX Shoes", Price = 99.00M, Url = "/Content/images/salomon-106079-aw10a-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 16, Category = category, Name = "Mizuno Ladies Wave Alchemy 10 Shoes", Price = 76.50M, Url = "/Content/images/mizuno-08KN067-21-ind.jpg" };
            //    StoreProduct(db, product);

            //    category = new Category { CategoryId = 5, Name = "Nutrition" };
            //    StoreCategory(db, category);

            //    product = new Product { ProductId = 17, Category = category, Name = "For Goodness Shakes Recovery Powder 24x80g", Price = 28.80M, Url = "/Content/images/fgs-powder-10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 18, Category = category, Name = "High5 EnergySource 4:1 With Super Carbs 1.6kg Tub", Price = 25.19M, Url = "/Content/images/sis-H5962-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 19, Category = category, Name = "High5 Protein Recovery 1.6kg Tub", Price = 26.34M, Url = "/Content/images/high5.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 20, Category = category, Name = "Science in Sport PSP22 High Energy Sports Fuel 1.6kg Tub", Price = 18.00M, Url = "/Content/images/sis-SIS50014-ind.jpg" };
            //    StoreProduct(db, product);

            //    category = new Category { CategoryId = 6, Name = "Accessories" };
            //    StoreCategory(db, category);

            //    product = new Product { ProductId = 21, Category = category, Name = "Garmin Edge 500 with Heart Rate, Cadence and Team Jersey", Price = 251.99M, Url = "/Content/images/garmin-010-00829-05-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 22, Category = category, Name = "Giro Prolight Road Helmet", Price = 134.99M, Url = "/Content/images/giro-prolight-ht-10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 23, Category = category, Name = "Fox Flux Helmet", Price = 70.00M, Url = "/Content/images/fox-flux-helmet-10-ind.jpg" };
            //    StoreProduct(db, product);
            //    product = new Product { ProductId = 24, Category = category, Name = "Fibre Flare Shorty Rear Light", Price = 26.99M, Url = "/Content/images/fibre-flare-shorty-ind.jpg" };
            //    StoreProduct(db, product);

            //}
            //finally
            //{
            //    db.Close();
            //}

        }
    }
}