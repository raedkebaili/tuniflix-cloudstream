// Exemple de plugin Tuniflix pour Cloudstream
using CloudstreamAPI;

public class Tuniflix : ISource {
    public string Name => "Tuniflix"; // Nom du plugin
    public string IconUrl => "https://tuniflix.site/wp-content/uploads/2025/01/TUNIFLIX-1-25-2025-2.png"; // URL de l'icône
    public string BaseUrl => "https://tuniflix.site"; // URL de base du site

    // Fonction pour récupérer les films basés sur une recherche
    public List<Movie> FetchMovies(string query) {
        var movies = new List<Movie>();
        
        // Exemple de récupération des films : utiliser un extracteur pour rechercher des films
        var searchUrl = $"{BaseUrl}/?s={query}"; // Requête de recherche
        
        // Implémentation pour extraire les films de la page
        var document = GetDocument(searchUrl); // Méthode pour récupérer le document HTML de la page
        
        // Analyse du document et récupération des films
        var movieElements = document.QuerySelectorAll("div.movie-item");
        
        foreach (var movieElement in movieElements) {
            var title = movieElement.QuerySelector("h3").TextContent;
            var link = movieElement.QuerySelector("a").GetAttribute("href");
            var poster = movieElement.QuerySelector("img").GetAttribute("src");

            movies.Add(new Movie {
                Title = title,
                Link = link,
                Poster = poster
            });
        }
        
        return movies;
    }

    // Fonction pour récupérer les séries basées sur une recherche
    public List<Serie> FetchSeries(string query) {
        var series = new List<Serie>();
        
        // Exemple de récupération des séries : utiliser un extracteur pour rechercher des séries
        var searchUrl = $"{BaseUrl}/?s={query}"; // Requête de recherche
        
        // Implémentation pour extraire les séries de la page
        var document = GetDocument(searchUrl); // Méthode pour récupérer le document HTML
        
        var serieElements = document.QuerySelectorAll("div.serie-item");
        
        foreach (var serieElement in serieElements) {
            var title = serieElement.QuerySelector("h3").TextContent;
            var link = serieElement.QuerySelector("a").GetAttribute("href");
            var poster = serieElement.QuerySelector("img").GetAttribute("src");

            series.Add(new Serie {
                Title = title,
                Link = link,
                Poster = poster
            });
        }
        
        return series;
    }

    // Fonction pour récupérer le document HTML d'une page
    private IDocument GetDocument(string url) {
        // Exemple de méthode pour récupérer un document HTML à partir de l'URL
        return HttpClient.Get(url).ToDocument();
    }
}
