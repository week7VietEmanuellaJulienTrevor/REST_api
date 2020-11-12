using System;


public class Lead

{
    public long Id { get; set; }
    public string contact_full_name { get; set; }
    public string company_name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string project_name { get; set; }
    public string project_description { get; set; }
    public string department { get; set; }
    public string message { get; set; }
    // public string attached_file { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string attached_file_path { get; set; }
}