# ProductCatalogApi

Wersja API: v1

Krótski opis

ProductCatalogApi to proste API katalogu produktów (wersja v1). Umożliwia listowanie produktów, pobieranie pojedynczego produktu, tworzenie nowych pozycji oraz wywołanie endpointu DELETE (zdefiniowanego w kontrolerze), który w aktualnej implementacji zwraca 204 No Content, ale nie modyfikuje przechowywanej listy. Działania są wykonywane w pamięci (statyczna lista w kontrolerze) — dane nie są zapisywane do bazy.

Base URL

Domyślny URL aplikacji zależy od konfiguracji hosta (np. http://{HOST}). Kontroler jest mapowany na ścieżkę /products.

Autoryzacja

Brak — API nie wymaga uwierzytelnienia. (Zgodnie z aktualną implementacją kontrolera w repozytorium.)

Zakres funkcji

ProductCatalogApi v1 pozwala na:
- listowanie produktów (GET)
- pobieranie pojedynczego produktu (GET /{id})
- tworzenie nowego produktu (POST)
- wywołanie endpointu DELETE /products (aktualnie zwraca 204 No Content; nie usuwa elementów)

Modele

Model: Product
- id (int) — identyfikator produktu
- name (string) — nazwa produktu
- category (string) — kategoria produktu
- price (decimal) — cena produktu
- isActive (bool) — flaga aktywności produktu

Przykładowe JSON (lista / przykładowe elementy):

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

Endpointy

1) Pobierz listę produktów
- Metoda: GET
- Ścieżka: /products
- Parametry: brak
- Odpowiedź: 200 OK — tablica obiektów Product

2) Pobierz produkt po id
- Metoda: GET
- Ścieżka: /products/{id}
- Parametry: id (int) — identyfikator produktu
- Odpowiedź:
  - 200 OK — zwraca obiekt Product gdy produkt istnieje
  - 404 Not Found — gdy produkt o podanym id nie istnieje

3) Utwórz nowy produkt
- Metoda: POST
- Ścieżka: /products
- Body: JSON (Product) — obiekt produktu bez pola id (id jest nadawane automatycznie przez kontroler)
- Odpowiedź:
  - 201 Created — lokalizacja nowo utworzonego zasobu /products/{id}, odpowiedź zawiera utworzony obiekt Product

4) (Istniejący w kontrolerze) Wywołanie DELETE
- Metoda: DELETE
- Ścieżka: /products
- Parametry: brak
- Odpowiedź:
  - 204 No Content — aktualna implementacja zwraca NoContent() i nie modyfikuje statycznej listy produktów w kontrolerze.

Uwaga dotycząca DELETE

Kontroler zawiera metodę obsługującą żądanie HTTP DELETE na ścieżce /products (atrybut [HttpDelete]). Jednak ta metoda:
- nie przyjmuje identyfikatora produktu (brak parametru id w sygnaturze),
- nie usuwa ani nie modyfikuje listy produktów przechowywanej w pamięci,
- jedynie zwraca 204 No Content.

Oznacza to, że nie istnieje możliwość usuwania pojedynczych produktów przez id, ani faktycznego usunięcia zasobów przez wywołanie tego endpointu w obecnym kodzie.

Przykłady cURL

1) Pobierz listę produktów

curl -sS -X GET "http://{HOST}/products"

2) Pobierz produkt po id=1

curl -sS -X GET "http://{HOST}/products/1"

3) Utwórz nowy produkt

curl -sS -X POST "http://{HOST}/products" -H "Content-Type: application/json" -d '{"name":"Nowy Produkt","category":"Kategoria","price":123.45,"isActive":true}'

4) Wywołanie DELETE (obecna implementacja)

curl -sS -X DELETE "http://{HOST}/products"

Oczekiwana odpowiedź: 204 No Content — metoda istnieje w kontrolerze, ale nie powoduje usunięcia pozycji.

Uwagi implementacyjne i ograniczenia

- Dane są przechowywane w pamięci w postaci statycznej listy w kontrolerze ProductCatalogApi.V1.Controllers.ProductsController. Po restarcie aplikacji lista wraca do wartości początkowych.
- Brak walidacji pól w modelu (np. cena >= 0) — aktualnie przyjmowane są dane przesyłane w body bez dodatkowej walidacji.
- Brak mechanizmu paginacji, filtrowania lub sortowania.
- Endpoint DELETE istnieje, ale nie realizuje usuwania zasobów. Jeśli celem jest dodanie funkcjonalnego usuwania, mogę przygotować propozycję implementacji (np. DELETE /products/{id} usuwające element z listy) do rozważenia.

Kod źródłowy (lokacje)

- Kod kontrolera: Controllers/ProductsController.cs
- Model danych: Models/Product.cs
- Dokument wymagań: Documentation/DocumentationRequirements.md

Historia zmian

- 2026-09-03 — Aktualizacja: zaznaczono brak endpointu DELETE i dodano historię zmian.
- 2026-09-03 — Aktualizacja: zaktualizowano dokumentację na podstawie aktualnych plików źródłowych; dopisano opis istniejącego endpointu DELETE (zwracającego 204 No Content, nie modyfikującego listy).
