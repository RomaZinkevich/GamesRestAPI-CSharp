﻿namespace TestWebApplication;

public record class UpdateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);

