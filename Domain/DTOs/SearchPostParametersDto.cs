namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Title { get; }

    public SearchPostParametersDto(string? title)
    {
        Title = title;
    }
}