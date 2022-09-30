namespace WpfProjectTemplate.Primitives.Abstract;

public interface IYesOrNoDialog
{
    bool AskQuestion(string title, string message);
}