# ProductCatalogApi

Wersja API: v1

Krótki opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Udostępnia listę produktów przechowywanych w pamięci aplikacji i pozwala na pobieranie, dodawanie oraz usuwanie produktów. Projekt pierwotnie przygotowany jako integracja / źródło danych dla n8n.

Base URL

Wszystkie ścieżki są względne względem hosta aplikacji. Kontroler jest zmapowany na ścieżkę:

- /products

Autoryzacja

Brak — kontroler nie wymaga uwierzytelnienia (w kodzie nie występują atrybuty [Authorize]).

Zachowanie przechowywania danych

Dane przechowywane są w statycznej liście w pamięci procesu aplikacji (List<Product>). Oznacza to, że:
- dane nie są trwałe i po restarcie aplikacji zostaną utracone,
- API nie wspiera paginacji ani filtrowania (lista jest mała i statyczna).

Modele danych

Model: Product
- id (int) — identyfikator produktu,
- name (string) — nazwa produktu,
- category (string) — kategoria produktu,
- price (decimal) — cena produktu,
- isActive (bool) — flaga aktywności produktu.

(Uwaga: model nie został odnaleziony jako oddzielny plik Model w repozytorium; powyższe pola są wywnioskowane z użycia w kontrolerze.)

Lista endpointów (skrót)

- GET /products — pobiera listę wszystkich produktów
- GET /products/{id} — pobiera produkt o podanym id
- POST /products — tworzy nowy produkt (JSON w body)
- DELETE /products/{id} — usuwa produkt o podanym id

Szczegółowy opis endpointów

1) Pobierz wszystkie produkty
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Opis: Zwraca aktualną listę produktów przechowywanych w pamięci aplikacji.
- Odpowiedź: 200 OK — JSON: tablica obiektów Product.

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
  - 200 OK — zwrócono produkt
  - 404 Not Found — produkt o podanym id nie istnieje

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
- Body: JSON — obiekt Product (bez pola id; id jest nadawane po stronie serwera)

Przykład body (request):
{
  "name": "Nowy Produkt",
  "category": "Kategoria",
  "price": 123.45,
  "isActive": true
}

- Opis działania: Serwer tworzy nowy obiekt Product. Id jest generowane jako (max existing id) + 1 lub 1 gdy lista jest pusta. Nowy produkt jest dodawany do listy w pamięci.
- Kody odpowiedzi:
  - 201 Created — utworzono produkt; w nagłówku Location wskazuje na GET /products/{id}, w body znajduje się utworzony obiekt.

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
  - 204 No Content — produkt usunięty
  - 404 Not Found — brak produktu o podanym id

Przykłady użycia (curl)

1) Pobierz listę produktów

curl -sS -X GET "http://{HOST}/products" 

2) Pobierz produkt o id=1

curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt

curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) Usuń produkt o id=4

curl -sS -X DELETE "http://{HOST}/products/4"

Uwagi implementacyjne i ograniczenia

- API jest prostym przykładem i przechowuje dane tylko w pamięci procesu (statyczna lista). Nie zaleca się używania tej implementacji w produkcji bez dodania trwałego magazynu.
- Brak walidacji wejściowej i ograniczonego sprawdzania błędów. Możliwe rozszerzenia:
  - walidacja modelu (DataAnnotations),
  - obsługa błędów i ujednolicone odpowiedzi błędów,
  - paginacja, sortowanie i filtrowanie listy produktów,
  - integracja z bazą danych.

Schemat JSON modelu (opis)

Product (przykładowy schemat):
- id: integer (np. 1)
- name: string (np. "Laptop Pro 14")
- category: string (np. "Electronics")
- price: number (decimal) (np. 6499.00)
- isActive: boolean (true / false)

Kody odpowiedzi — podsumowanie

- 200 OK — operacja zakończona sukcesem (GET)
- 201 Created — zasób utworzony (POST)
- 204 No Content — zasób usunięty (DELETE)
- 404 Not Found — zasób nie istnieje

Kontakt / repozytorium

Repozytorium: https://github.com/ppnet1980/ProductCatalogApi
Autor / właściciel repozytorium: ppnet1980

Historia zmian

- 2026-09-03 — v1 — Dodanie dokumentacji początkowej na podstawie kodu źródłowego (ProductsController).
- 2026-09-03 — v1.1 — Uzupełnienie dokumentacji zgodnie z wymaganiami: dodanie przykładów curl, opis schematu JSON oraz sekcji "Uwagi implementacyjne i ograniczenia".

