﻿using AutoMapper;
using Core.Models.Themes;

namespace Core.Dto.Themes;

public class SuggestedThemeListDto
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public int UserId { get; set; }

    public List<KeyWordListDto> KeyWords { get; set; } = [];
}

public partial class SuggestedThemeListDtoProfile : Profile
{
    public SuggestedThemeListDtoProfile()
    {
        CreateMap<SuggestedTheme, SuggestedThemeListDto>();
    }
}
