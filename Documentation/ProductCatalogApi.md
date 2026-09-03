# ProductCatalogApi

Krótki opis

Product Catalog API (kontroler ProductsController) — proste REST API do zarządzania katalogiem produktów. Implementacja na podstawie ASP.NET Core (ApiController). Dane przechowywane są w pamięci (statyczna lista) — przy restarcie aplikacji dane wracają do wartości domyślnych.

Uwagi ogólne

- Repozytorium: ppnet1980/ProductCatalogApi
- Opis repozytorium: Product Catalog API for n8n
- Kontroler: ProductsController
- Ścieżka bazowa kontrolera: /products
- Brak uwierzytelniania w kodzie źródłowym
- Storage: w pamięci (statyczna lista Product)

Lista metod (endpoints)

1) GET /products
- Opis: Pobiera listę wszystkich produktów.
- Metoda: HttpGet
- Request body: brak
- Odpowiedź:
  - 200 OK — lista obiektów Product
- Przykładowa odpowiedź (200):

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
    "name": "Noise Canceling Headphones",
    "category": "Electronics",
    "price": 1299.00,
    "isActive": false
  }
]

2) GET /products/{id}
- Opis: Pobiera produkt o podanym identyfikatorze.
- Metoda: HttpGet, routing parametryczny {id:int}
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Odpowiedzi:
  - 200 OK — obiekt Product
  - 404 Not Found — gdy produkt o podanym id nie istnieje (zwracany jest komunikat tekstowy)
- Przykład odpowiedzi 404 (treść zwracana z kodu):

"Nie znaleziono produktu o ID = {id}."

3) POST /products
- Opis: Tworzy nowy produkt.
- Metoda: HttpPost
- Request body: JSON reprezentujący Product (pole id jest ignorowane przy tworzeniu — serwer nada nowe id)
- Logika tworzenia id: jeżeli lista jest pusta -> id = 1, w przeciwnym wypadku id = max(existing ids) + 1
- Odpowiedzi:
  - 201 Created — nagłówek Location wskazuje na GET /products/{id}, w treści zwracany jest utworzony obiekt
- Przykładowe żądanie (body):

{
  "name": "New Product",
  "category": "Gadgets",
  "price": 199.99,
  "isActive": true
}

- Przykładowa odpowiedź 201 (body):

{
  "id": 4,
  "name": "New Product",
  "category": "Gadgets",
  "price": 199.99,
  "isActive": true
}

4) PATCH /products/{id}/status
- Opis: Aktualizuje pole isActive (status aktywności) dla produktu o podanym id.
- Metoda: HttpPatch
- Routing: {id:int}/status
- Request body: prosty JSON/tekst reprezentujący wartość boolean (w kodzie [FromBody] bool isActive)
- Odpowiedzi:
  - 200 OK — zwraca zaktualizowany obiekt Product
  - 404 Not Found — gdy produkt o podanym id nie istnieje (zwracany jest komunikat tekstowy)

Przykład żądania PATCH (body):
true

Przykład odpowiedzi 404 (treść zwracana z kodu):
"Nie znaleziono produktu o ID = {id}."

Model danych: Product

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public bool IsActive { get; set; }
}

Uwagi techniczne i ograniczenia

- Dane przechowywane są wyłącznie w pamięci procesu (static List<Product> Products). Nie ma bazy danych ani trwałego przechowywania w kodzie źródłowym.
- Przy restarcie aplikacji lista przywracana jest do wartości domyślnych zawartych w kodzie.
- Brak paginacji, filtrowania lub sortowania — GET /products zwraca całą listę.
- Brak walidacji pól (np. brak sprawdzenia czy price >= 0) — walidacja nie jest zaimplementowana w kontrolerze.
- Brak mechanizmów autoryzacji/uwierzytelniania w kodzie.

Przykłady użycia (curl)

- Pobierz wszystkie produkty:
  curl -X GET "http://<host>:<port>/products"

- Pobierz produkt o id = 1:
  curl -X GET "http://<host>:<port>/products/1"

- Utwórz nowy produkt:
  curl -X POST "http://<host>:<port>/products" -H "Content-Type: application/json" -d '{"name":"New","category":"G","price":9.99,"isActive":true}'

- Zaktualizuj status produktu (ustaw isActive = false):
  curl -X PATCH "http://<host>:<port>/products/2/status" -H "Content-Type: application/json" -d 'false'

Historia zmian

- 2026-09-03 — Utworzono dokumentację na podstawie kodu źródłowego (ProductsController i modele). Pierwsza wersja.

Uwagi końcowe

Dokumentacja oparta wyłącznie na źródłach kodu dostępnych w repozytorium. Nie wprowadzano żadnych przypuszczeń odnośnie dodatkowych endpointów, mechanizmów uwierzytelniania ani zewnętrznych zależności. Jeśli chcesz, mogę dopisać przykłady odpowiedzi z pełnymi nagłówkami HTTP lub rozwinąć sekcję błędów/validation po dodaniu odpowiedniego kodu do repozytorium.