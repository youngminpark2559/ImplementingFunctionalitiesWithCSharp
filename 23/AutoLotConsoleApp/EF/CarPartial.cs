namespace AutoLotConsoleApp.EF
{
    //Added a partial class Car to override ToString() to define special functionality, in this case for string format and value for null in CarNickName.
    public partial class Car
    {
        //Overrides Car.ToString() to define special functionality, in this case for string format and value for null in CarNickName.
        public override string ToString()
        {
            //Since the PetName column could be empty, 
            //supply the default name of **No Name**.
            //**No Name** is a Blue BMW with ID3
            //Bim is a Green BMW with ID4
            return $"{this.CarNickName ?? "**No Name**"} is a {this.Color} {this.Make} with ID { this.CarId}.";
        }
    }
}