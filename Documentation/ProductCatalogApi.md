# ProductCatalogApi

Krótki opis

Product Catalog API (kontroler ProductsController) — proste REST API do zarządzania katalogiem produktów. Implementacja na podstawie ASP.NET Core (ApiController). Dane przechowywane są w pamięci (statyczna lista) — przy restarcie aplikacji dane wracają do wartości domyślnych.

Uwagi ogólne

- Repozytorium: ppnet1980/ProductCatalogApi
- Opis repozytorium: Product Catalog API for n8n
- Kontroler: ProductsController (namespace ProductCatalogApi.V1.Controllers)
- Ścieżka bazowa kontrolera: /products
- Brak uwierzytelniania w kodzie źródłowym
- Storage: w pamięci (static List<Product> Products, inicjalizowana w kontrolerze)

Lista metod (endpoints)

1) GET /products
- Atrybut: [HttpGet]
- Opis: Pobiera listę wszystkich produktów.
- Request body: brak
- Odpowiedź:
  - 200 OK — lista obiektów Product w treści (JSON)
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
- Atrybut: [HttpGet("{id:int}")]
- Opis: Pobiera produkt o podanym identyfikatorze.
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Odpowiedzi:
  - 200 OK — obiekt Product (JSON)
  - 404 Not Found — gdy produkt o podanym id nie istnieje; w kontrolerze zwracane jest NotFound() bez treści w body

3) POST /products
- Atrybut: [HttpPost]
- Opis: Tworzy nowy produkt.
- Request body: JSON reprezentujący Product (pola przesyłane w body: Name, Category, Price, IsActive). Pole Id w przesyłanym obiekcie jest ignorowane przy tworzeniu — serwer nada nowe Id.
- Logika tworzenia Id: jeżeli lista jest pusta -> Id = 1, w przeciwnym wypadku Id = max(existing ids) + 1
- Odpowiedzi:
  - 201 Created — nagłówek Location wskazuje na GET /products/{id} (CreatedAtAction(nameof(GetById), new { id = createdId }, createdObject)), w treści zwracany jest utworzony obiekt (JSON)
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

Model danych: Product (namespace ProductCatalogApi.V1.Models)

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public bool IsActive { get; set; }
}

Uwagi techniczne i ograniczenia

- Dane przechowywane są wyłącznie w pamięci procesu (static List<Product> Products zainicjalizowana w kontrolerze). Nie ma bazy danych ani trwałego przechowywania w kodzie źródłowym.
- Przy restarcie aplikacji lista przywracana jest do wartości domyślnych zawartych w kodzie.
- Brak paginacji, filtrowania lub sortowania — GET /products zwraca całą listę.
- Brak walidacji pól (np. brak sprawdzenia czy Price >= 0) — walidacja nie jest zaimplementowana w kontrolerze.
- Brak mechanizmów autoryzacji/uwierzytelniania w kodzie.
- GET /products/{id} zwraca NotFound() bez treści, gdy produkt nie istnieje.
- POST tworzy nowy obiekt Product na podstawie przesłanego body i zwraca CreatedAtAction wskazujące na akcję GetById.

Przykłady użycia (curl)

- Pobierz wszystkie produkty:
  curl -X GET "http://<host>:<port>/products"

- Pobierz produkt o id = 1:
  curl -X GET "http://<host>:<port>/products/1"

- Utwórz nowy produkt:
  curl -X POST "http://<host>:<port>/products" -H "Content-Type: application/json" -d '{"name":"New","category":"G","price":9.99,"isActive":true}'

Historia zmian

- 2026-09-03 — Utworzono dokumentację na podstawie kodu źródłowego (ProductsController i modele). Pierwsza wersja.
- 2026-09-03 — Zaktualizowano dokumentację: usunięto opis nieistniejącego endpointu PATCH, doprecyzowano zachowanie odpowiedzi 404 i 201.
- 2026-09-03 — (synchronizacja) Uaktualniono dokumentację w oparciu o aktualne pliki źródłowe z repozytorium: potwierdzenie listy endpointów (GET all, GET by id, POST), potwierdzenie inicjalizacji danych w kontrolerze oraz szczegółów CreatedAtAction i NotFound().

Uwagi końcowe

Dokumentacja oparta wyłącznie na źródłach kodu dostępnych w repozytorium. Nie wprowadzono żadnych przypuszczeń odnośnie dodatkowych endpointów, mechanizmów uwierzytelniania ani zewnętrznych zależności. Jeżeli chcesz, mogę:
- dopisać pełne przykłady odpowiedzi HTTP z nagłówkami,
- rozszerzyć sekcję błędów po dodaniu logiki walidacji,
- wygenerować specyfikację OpenAPI/Swagger ręcznie na podstawie kodu.
