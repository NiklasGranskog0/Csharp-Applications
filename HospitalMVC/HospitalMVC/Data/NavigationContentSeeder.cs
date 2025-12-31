using HospitalMVC.Models;

namespace HospitalMVC.Data;

public static class NavigationContentSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.NavigationContents.Any()) return;
        
        context.NavigationContents.AddRange(
            new NavigationContent { Category = "Nutrition", Content = "<li>Eat a balanced diet with whole foods, fruits, vegetables, and lean proteins</li><li>Limit processed foods, added sugars, and excess salt</li>" },
            new NavigationContent { Category = "Sleep", Content = "<li>Aim for 8 hours of quality sleep every night to support recovery and mental clarity</li><li>Keep a consistent bedtime routine and avoid screens before sleep</li>" },
            new NavigationContent { Category = "Children", Content = "<li>Support children's growth with healthy meals, physical activity, and regular health check-ups</li><li>Encourage play, learning, and social interaction for well-being</li>" },
            new NavigationContent { Category = "Pregnancy", Content = "<li>During pregnancy, attend prenatal visits, take recommended supplements, and maintain a nutrient-rich diet</li><li>Avoid alcohol, smoking, and high-risk foods to protect the baby</li>" },
            new NavigationContent { Category = "Diseases", Content = "<li>Protect against diseases through vaccination, hygiene, and early medical consultation when symptoms appear</li><li>Manage chronic conditions with regular monitoring and lifestyle adjustments</li>" },
            new NavigationContent { Category = "Ailments", Content = "<li>Address common ailments like colds or headaches with rest, hydration, and gentle remedies</li><li>Seek medical help if symptoms persist or worsen</li>" },
            new NavigationContent { Category = "Accidents", Content = "<li>Prevent accidents by using safety equipment, staying alert, and maintaining a safe environment at home and work</li><li>Learn basic first aid skills to handle emergencies</li>" },
            new NavigationContent { Category = "Injuries", Content = "<li>If injuries occur, seek professional care promptly and follow recovery plans or rehabilitation exercises</li><li>Allow proper healing time before resuming intense activities</li>" },
            new NavigationContent { Category = "Examinations", Content = "<li>Schedule regular examinations such as blood tests, dental check-ups, and screenings for early detection</li><li>Discuss family history and risk factors with your doctor</li>" },
            new NavigationContent { Category = "Treatments", Content = "<li>Adhere to prescribed treatments, whether medications, therapies, or lifestyle changes, for effective recovery</li><li>Track progress and communicate with healthcare providers regularly</li>" });
        context.SaveChanges();
    }
}