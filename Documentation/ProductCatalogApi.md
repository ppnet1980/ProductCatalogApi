# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja V1). Udostępnia listę produktów przechowywanych w pamięci aplikacji i pozwala na pobieranie, dodawanie oraz usuwanie produktów. Projekt pierwotnie przygotowany jako integracja / źródło danych dla n8n.

Base URL

Wszystkie ścieżki są względne względem hosta aplikacji. Kontroler jest zmapowany na ścieżkę:

- /products

Autoryzacja

Brak — kontroler nie wymaga uwierzytelnienia (brak atrybutów [Authorize] w kodzie).

Zachowanie przechowywania danych

Dane przechowywane są w statycznej liście w pamięci procesu aplikacji (List<Product>). Oznacza to, że:
- dane nie są trwałe i po restarcie aplikacji zostaną utracone,
- API nie wspiera paginacji ani filtrowania (lista jest mała i statyczna).

Modele danych

Model Product (używany w API)
- Id (int) — identyfikator produktu,
- Name (string) — nazwa produktu,
- Category (string) — kategoria produktu,
- Price (decimal) — cena produktu,
- IsActive (bool) — flaga aktywności produktu.

(Uwaga: definicja modelu nie została odnaleziona jako osobny plik w katalogu Models w repozytorium, ale pola są używane w kontrolerze i powyższe właściwości wynikają z wykorzystania w kodzie.)

Lista endpointów (skrót)

- GET /products — pobiera listę wszystkich produktów,
- GET /products/{id} — pobiera produkt o podanym id,
- POST /products — tworzy nowy produkt (JSON w body),
- DELETE /products/{id} — usuwa produkt o podanym id.

Szczegółowy opis endpointów

1) Pobierz wszystkie produkty
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Opis: Zwraca aktualną listę produktów.
- Odpowiedź: 200 OK, JSON tablica obiektów Product.

Przykład odpowiedzi (fragment):
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
  }
]

2) Pobierz produkt po identyfikatorze
- Metoda: GET
- Ścieżka: /products/{id}
- Parametry ścieżki:
  - id (int) — identyfikator produktu
- Opis: Zwraca obiekt Product o wskazanym id.
- Kody odpowiedzi:
  - 200 OK — zwrócono produkt,
  - 404 Not Found — produkt o podanym id nie istnieje.

Przykład odpowiedzi (200):
{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": true
}

3) Utwórz nowy produkt
- Metoda: POST
- Ścieżka: /products
- Body: JSON — obiekt Product (pola oprócz Id, Id jest ustawiane po stronie serwera)

Przykład body (request):
{
  "name": "Nowy Produkt",
  "category": "Kategoria",
  "price": 123.45,
  "isActive": true
}

- Opis działania: Serwer tworzy nowy obiekt Product. Id jest generowane jako (max existing id) + 1 lub 1 gdy lista jest pusta. Nowy produkt jest dodawany do listy w pamięci.
- Kody odpowiedzi:
  - 201 Created — utworzono produkt; w nagłówkach Location wskazuje na GET /products/{id}, w body znajduje się utworzony obiekt.

Przykład odpowiedzi (201):
Status: 201 Created
Header: Location: /products/4
Body:
{
  "id": 4,
  "name": "Nowy Produkt",
  "category": "Kategoria",
  "price": 123.45,
  "isActive": true
}

4) Usuń produkt
- Metoda: DELETE
- Ścieżka: /products/{id}
- Parametry ścieżki:
  - id (int) — identyfikator produktu do usunięcia
- Opis: Jeśli produkt istnieje — usuwa go z listy i zwraca 204 No Content. Jeśli nie istnieje — zwraca 404 Not Found.
- Kody odpowiedzi:
  - 204 No Content — produkt usunięty,
  - 404 Not Found — brak produktu o podanym id.

Uwagi implementacyjne

- API używa przestrzeni nazw V1 (ProductCatalogApi.V1.Controllers), stąd wersja v1.
- Dane są przechowywane w pamięci w statycznej liście w kontrolerze (niezależnie od zewnętrznej bazy danych).
- Brak walidacji wejściowej w kontrolerze — przydatne rozszerzenia to: walidacja modelu (DataAnnotations), obsługa błędów walidacji, paginacja, filtrowanie oraz trwałe przechowywanie danych.

Historia zmian

- 2026-09-03 — v1 — Dodanie dokumentacji początkowej na podstawie kodu źródłowego (ProductsController).

