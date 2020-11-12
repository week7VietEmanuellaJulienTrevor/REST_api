using System;

public class Column
{
    private DateTime _date = DateTime.Now;

    public long Id { get; set; }
    public string type_of_building { get; set; }
    public int number_of_floors_served { get; set; }
    public string status { get; set; }
    public string information { get; set; }
    public string notes { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get {return _date; } set { _date = value; } }
    public long battery_id { get; set; }
    public long customer_id { get; set; }
}

// public class ColumnStatus
// {
//     public long Id { get; set; }
//     public string status { get; set; }
//     public ColumnStatus(long id, string Status)
//     {
//         Id = id;
//         status = Status;
//     }
    
// };