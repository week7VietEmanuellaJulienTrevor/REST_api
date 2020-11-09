using System;

public class Battery
{
    public long Id { get; set; }
    public string building_id { get; set; } 
    public string type_of_building { get; set; }
    public string status { get; set; }
    public string employee_id { get; set; }
    //public DateTime commissioning_date { get; set; }
    //public DateTime last_inspection_date { get; set; }
    public string operations_certificate { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    // created_at          | updated_at  NOT TO BE MODIFIED
    public long customer_id { get; set; }

}