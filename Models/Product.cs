namespace Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductURL { get; set; }
        public byte[] ProductIMG { get; set; }
        public string ProductIMGtype { get; set; }
        public Product() { }

        public Product(int productID, string productName, double productPrice, int productTypeID, string productURL, byte[] productIMG, string productIMGtype)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductTypeID = productTypeID;
            ProductURL = productURL;
            ProductIMG = productIMG;
            ProductIMGtype = productIMGtype;
        }

        public Product(int productID, string productName, double productPrice)
        {
            ProductID = productID;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductTypeID = 0;
            ProductURL = null;
            ProductIMG = null;
            ProductIMGtype = null;
        }
    }

}