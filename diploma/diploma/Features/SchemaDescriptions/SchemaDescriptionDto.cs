﻿using AutoMapper;

namespace diploma.Features.SchemaDescriptions;

public class SchemaDescriptionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<SchemaDescriptionFileDto> Files { get; set; } = null!;
}

public class SchemaDescriptionFileDto
{
    public string Dbms { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool HasProblems { get; set; }
    public string? Problems { get; set; }
}

public class SchemaDescriptionProfile : Profile
{
    public SchemaDescriptionProfile()
    {
        CreateMap<SchemaDescription, SchemaDescriptionDto>()
            .ForMember(d => d.Files, o => o.MapFrom(s => s.Files));
        CreateMap<SchemaDescriptionFile, SchemaDescriptionFileDto>()
            .ForMember(d => d.Description, o => o.MapFrom(s => File.ReadAllText(s.FilePath)));
    }
}