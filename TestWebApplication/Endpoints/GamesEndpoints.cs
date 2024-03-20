using TestWebApplication.Dtos;

namespace TestWebApplication.Endpoints;

public static class GamesEndpoints
{

    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new GameDto(
            1,
            "Final Fantasy XIV",
            "Roleplaying",
            59.99M,
            new DateOnly(2010, 9, 30)),
        new GameDto(
            2,
            "The Legend of Zelda: Breath of the Wild",
            "Action-adventure",
            59.99M,
            new DateOnly(2017, 3, 3)),
        new GameDto(
            3,
            "The Witcher 3: Wild Hunt",
            "Action role-playing",
            39.99M,
            new DateOnly(2015, 5, 19)),
        new GameDto(
            4,
            "Red Dead Redemption 2",
            "Action-adventure",
            59.99M,
            new DateOnly(2018, 10, 26)),
        new GameDto(
            5,
            "Persona 5",
            "Role-playing",
            49.99M,
            new DateOnly(2016, 9, 15)),
        new GameDto(
            6,
            "Cyberpunk 2077",
            "Action role-playing",
            49.99M,
            new DateOnly(2020, 12, 10)),
        new GameDto(
            7,
            "Horizon Zero Dawn",
            "Action role-playing",
            49.99M,
            new DateOnly(2017, 2, 28)),
        new GameDto(
            8,
            "God of War",
            "Action-adventure",
            39.99M,
            new DateOnly(2018, 4, 20)),
        new GameDto(
            9,
            "The Elder Scrolls V: Skyrim",
            "Action role-playing",
            39.99M,
            new DateOnly(2011, 11, 11))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) => {

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
