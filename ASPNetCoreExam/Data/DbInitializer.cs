using ASPNetCoreExam.Models;
using System;
using System.Linq;

namespace ASPNetCoreExam.Data
{
    public class DbInitializer
    {
        public static void Initialize(StreamingPlatformContext context)
        {
            context.Database.EnsureCreated();

            if (context.StreamingPlatforms.Any())
            {
                return;
            }

            var streamingPlatforms = new StreamingPlatform[]
            {
            new StreamingPlatform{Name="HBO GO", Date=new DateTime(2021,08,25), ImageName="HBO_GO.png", Description="HBO Go is an international TV Everywhere video on demand streaming service offered by the American premium cable network HBO for customers outside the United States. It allowed HBO subscribers to stream selections of HBO content, including current and past series, films, specials, and sporting events, through either the HBO website, or apps on mobile devices, video game consoles, and digital media players."},
            new StreamingPlatform{Name="Netflix", Date=new DateTime(2021,08,26), ImageName="Netflix.png", Description="Netflix, Inc. is an American over-the-top content platform and production company headquartered in Los Gatos, California. Netflix was founded in 1997 by Reed Hastings and Marc Randolph in Scotts Valley, California. The company's primary business is a subscription-based streaming service offering online streaming from a library of films and television series, including those produced in-house."},
            new StreamingPlatform{Name="Amazon Prime", Date=new DateTime(2021,08,27), ImageName="Amazon.png", Description="Amazon Prime is a paid subscription program from Amazon which is available in various countries and gives users access to additional services otherwise unavailable or available at a premium to other Amazon customers. Services include same, one or two-day delivery of goods and streaming music, video, e-books, gaming and grocery shopping services."}
            };
            foreach (StreamingPlatform s in streamingPlatforms)
            {
                context.StreamingPlatforms.Add(s);
            }
            context.SaveChanges();

        }
    }
}
