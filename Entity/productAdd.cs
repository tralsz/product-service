namespace product_service.Entity
{
    public class productAdd
    {
            public string name { get; set; }
			public string Description { get; set; }
			public string ImageUrl { get; set; }
        public productAdd(string name, string Description, string ImageUrl )
            {
                this.name = name;
				this.Description = Description;
            this.ImageUrl = ImageUrl;
            }
        }
    
}