namespace product_service.Entity
{
    public class productsInfo
    {

            public int id { get; set; }
            public string name { get; set; }
        public productsInfo(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    
}
