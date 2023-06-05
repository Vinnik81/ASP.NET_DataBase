namespace News_2.Models
{
    public static class BlogDbInitializer
    {
       public static void seed(IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<NewsDbContext>();

            if (!context.Categories.Any()) 
            {
                context.Categories.Add(new Category() { Name = "Sport" });
                context.Categories.Add(new Category() { Name = "Games" }); 
                context.Categories.Add(new Category() { Name = "Study" });
                context.Categories.Add(new Category() { Name = "Funny" });
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                context.Tags.Add(new Tag() { Name = "One" });
                context.Tags.Add(new Tag() { Name = "Two" });
                context.Tags.Add(new Tag() { Name = "Free" });
                context.Tags.Add(new Tag() { Name = "Four" });
                context.SaveChanges();
            }
        }
    }
}
