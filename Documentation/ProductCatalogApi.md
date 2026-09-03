# ProductCatalogApi — Dokumentacja API (wszystkie wersje)

Data wygenerowania: 2026-09-03

Krótki opis

ProductCatalogApi to proste API HTTP (ASP.NET Core) udostępniające zasoby typu Product. Kontroler zdefiniowany w przestrzeni nazw ProductCatalogApi.V1.Controllers (ApiController) to aktywność [ApiController] oraz atrybuty [Route("products")], co oznacza, że wszystkie operacje dostępne są pod podstawowym route /products.

Ogólne informacje o uruchomieniu

Plik Program.cs uruchamia kontroler i mapuje routingi (app.MapControllers()). Aplikacja używa hostingu ASP.NET Core i odpowiada jako prosty endpoint HTTP.

Wymagania / Założenia dokumentacyjne

Dokumentacja opisuje metody wystawione przez kontroler ProductsController na podstawie aktualnego kodu źródłowego w repozytorium.

Autoryzacja / Zabezpieczenia

Brak mechanizmu autoryzacji lub uwierzytelniania w kodzie (brak atrybutów [Authorize]). Endpointy są publiczne.

Przechowywanie danych

Dane przechowywane są w pamięci aplikacji: statyczna lista Products (List<Product>) z kilkoma przykładowymi wpisami inicjalizowanymi w kodzie. Nie ma trwałego magazynu danych — restart aplikacji resetuje listę.

Seed danych (przykład zawartości listy Products)

- { "Id": 1, "Name": "Laptop Pro 14", "Category": "Electronics", "Price": 6499.00, "IsActive": true }
- { "Id": 2, "Name": "Office Chair Comfort", "Category": "Furniture", "Price": 899.00, "IsActive": true }
- { "Id": 3, "Name": "Noise Cancelling Headphones", "Category": "Electronics", "Price": 1299.00, "IsActive": false }

Lista metod (endpoints)

1) GET /products
- Opis: Pobiera listę wszystkich produktów.
- Metoda: HTTP GET
- Ścieżka: /products
- Parametry: brak
- Parametry nagłówka: brak
- Odpowiedzi:
  - 200 OK — tablica JSON obiektów Product
- Przykład odpowiedzi (200 OK):
[
  {
    "id": 1,
    "name": "Laptop Pro 14",
    "category": "Electronics",
    "price": 6499.00,
    "isActive": true
  },
  {
    "id": 2,
    "name": "Office Chair Comfort",
    "category": "Furniture",
    "price": 899.00,
    "isActive": true
  },
  {
    "id": 3,
    "name": "Noise Cancelling Headphones",
    "category": "Electronics",
    "price": 1299.00,
    "isActive": false
  }
]

2) GET /products/{id}
- Opis: Pobiera szczegóły produktu o podanym identyfikatorze.
- Metoda: HTTP GET
- Ścieżka: /products/{id} (id — liczba całkowita)
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Odpowiedzi:
  - 200 OK — pojedynczy obiekt Product
  - 404 Not Found — jeśli produkt o podanym id nie istnieje
- Przykład odpowiedzi (200 OK):
{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": true
}

3) POST /products
- Opis: Tworzy nowy produkt i dodaje go do listy (w pamięci).
- Metoda: HTTP POST
- Ścieżka: /products
- Treść żądania (Body): obiekt JSON zgodny z modelem Product (bez pola Id — Id jest nadawane przez serwer). Przykład:
{
  "name": "Nowy Produkt",
  "category": "Electronics",
  "price": 199.99,
  "isActive": true
}
- Odpowiedzi:
  - 201 Created — utworzony obiekt (Location wskazuje GET /products/{id})
  - 400 Bad Request — (brak szczegółowego walidatora w kodzie; standardowe błędy modelu mogą zwrócić 400)
- Przykład odpowiedzi (201 Created):
{
  "id": 4,
  "name": "Nowy Produkt",
  "category": "Electronics",
  "price": 199.99,
  "isActive": true
}

4) PATCH /products/{id}/status
- Opis: Aktualizuje pole IsActive dla produktu o podanym id.
- Metoda: HTTP PATCH
- Ścieżka: /products/{id}/status (id — liczba całkowita)
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Treść żądania (Body): prosty JSON-owy boolean (wartość pola IsActive). Ważne: w kodzie akcja przyjmuje [FromBody] bool isActive, zatem body powinno zawierać wartość true albo false (np. true).
- Odpowiedzi:
  - 200 OK — zwraca zaktualizowany obiekt Product
  - 404 Not Found — jeśli produkt o podanym id nie istnieje
- Przykład żądania (PATCH body):
true
- Przykład odpowiedzi (200 OK):
{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": false
}

Model: Product
- Id: int
- Name: string
- Category: string
- Price: decimal
- IsActive: bool

Przykładowy obiekt JSON Product (pełny):
{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": true
}

Uwagi implementacyjne
- W repozytorium kontroler wykorzystuje prostą, statyczną listę Product (Products) jako źródło danych. Tworzenie produktu automatycznie ustawia Id na (max existing Id + 1) lub 1 jeśli lista jest pusta.
- Endpoint PATCH przyjmuje prosty boolean w body zamiast obiektu z polem IsActive — klient powinien wysłać surową wartość boolean (np. true).
- Brak mechanizmów paginacji, filtrowania czy walidacji poza domyślną walidacją modelu ASP.NET Core.

Historia zmian
- 2026-09-03 — Utworzono dokumentację (wygenerowano automatycznie z kodu źródłowego).
- 2026-09-03 — Zaktualizowano dokumentację: dodano opis endpointu PATCH /products/{id}/status, uzupełniono informacje o kodach odpowiedzi oraz przykładach (aktualizacja związana z zamkniętym PR #10, gałąź ppnet1980-patch-9).

Generowane automatycznie z repozytorium: ppnet1980/ProductCatalogApi
