namespace InterviewProject.Dtos
{
    public class UpdateInterviewDto
        : CreateInterviewDto
    {
        // в моделях данных нужно использовать свойства
        // после этого фикса тесты UpdateInterview отработали успешно
        public int Id { get; set; }
    }
}
