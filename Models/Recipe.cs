namespace AllSpice.Models;


public class Recipe
{

    public string Id { get; set; }

    public string Title { get; set; }

    public string Instructions { get; set; }

    public string Category { get; set; }

    public string CreatorId { get; set; }

    public Account Creator { get; set; }





}