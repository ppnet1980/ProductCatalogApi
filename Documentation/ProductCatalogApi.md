# ProductCatalogApi — Dokumentacja API (wstępna)

Data generacji: 2026-09-03

Uwaga: dokumentacja została wygenerowana automatycznie na podstawie kodu źródłowego z repozytorium. Zawiera tylko te informacje, które można bezpośrednio wywnioskować z kodu — nie dodano żadnych założeń ani nowych metod.

## Krótkie wprowadzenie
ProduktCatalogApi to prosta usługa HTTP (ASP.NET Core) udostępniająca zasoby typu Product. Kontroler zdefiniowany w kodzie znajduje się w namespace ProductCatalogApi.V1.Controllers i ma atrybuty [ApiController] oraz [Route("products")], co oznacza, że ścieżki są relatywne do endpointu /products.

## Ogólne informacje o uruchomieniu
Plik Program.cs rejestruje kontrolery i mapuje je do routingu (app.MapControllers()). Aplikację można uruchomić standardowo poleceniem `dotnet run` (w katalogu projektu) — kod używa domyślnego hostingu ASP.NET Core.

## Wymagania/Założenia wykryte w repozytorium
- Brak mechanizmu uwierzytelniania w kodzie kontrolera (brak atrybutów [Authorize] ani obsługi tokenów w kontrolerze).
- Kontroler wystawia zasoby pod ścieżką relatywną `/products`.
- Przykładowe dane znajdują się w kontrolerze (lista Products z trzema elementami) — użyte jako przykłady odpowiedzi.

## Wystawione metody / endpointy
Lista metod wykrytych w kontrolerze ProductsController:

1. GET /products
   - Opis: Pobiera listę wszystkich produktów.
   - Metoda: HTTP GET
   - Ścieżka: /products
   - Parametry: brak
   - Odpowiedź:
     - 200 OK — zwraca tablicę obiektów Product (możliwe wartości zawarte w kodzie źródłowym).
   - Przykładowa odpowiedź (JSON):

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

2. GET /products/{id}
   - Opis: Pobiera produkt o podanym identyfikatorze.
   - Metoda: HTTP GET
   - Ścieżka: /products/{id} (w kodzie atrybut: [HttpGet("{id:int}")])
   - Parametry:
     - id (ścieżkowy, int) — identyfikator produktu
   - Odpowiedź:
     - 200 OK — zwraca obiekt Product, jeśli istnieje.
     - 404 Not Found — jeśli produkt o podanym id nie istnieje.
   - Przykład odpowiedzi 200 (JSON):

     {
       "id": 1,
       "name": "Laptop Pro 14",
       "category": "Electronics",
       "price": 6499.00,
       "isActive": true
     }

3. POST /products
   - Opis: Tworzy nowy produkt na podstawie przesłanego ciała żądania.
   - Metoda: HTTP POST
   - Ścieżka: /products
   - Parametry:
     - Body (application/json) — obiekt Product (w praktyce klient powinien wysłać właściwości produktu poza Id; serwer ustawia Id automatycznie jako max(Id)+1 lub 1 gdy lista pusta).
   - Odpowiedź:
     - 201 Created — zwraca utworzony obiekt Product i ustawia nagłówek Location (CreatedAtAction wskazujące na GET /products/{id}).
   - Przykładowe ciało żądania (JSON):

     {
       "name": "Nowy Produkt",
       "category": "Electronics",
       "price": 199.99,
       "isActive": true
     }

   - Przykład odpowiedzi 201 (JSON):

     {
       "id": 4,
       "name": "Nowy Produkt",
       "category": "Electronics",
       "price": 199.99,
       "isActive": true
     }

   - Uwaga: Id jest generowane po stronie serwera (kod używa: Products.Count == 0 ? 1 : Products.Max(p => p.Id) + 1).

## Model: Product
Na podstawie użycia w kontrolerze wykryto następujące pola modelu Product (typy odczytane z inicjalizacji w kodzie):

- Id: int
- Name: string
- Category: string
- Price: decimal (wartości w kodzie z sufiksem m)
- IsActive: bool

Przykładowy obiekt Product (JSON):

{
  "id": 1,
  "name": "Laptop Pro 14",
  "category": "Electronics",
  "price": 6499.00,
  "isActive": true
}

## Ograniczenia i uwagi
- W repozytorium brak jest definicji klasy Product w folderze Models (w czasie analizy nie stwierdzono pliku modelu). Dokumentacja opisuje jedynie pola używane w kontrolerze. Jeśli klasa Product znajduje się w innym miejscu lub zostanie dodana, proszę zsynchronizować dokumentację.
- Brak obsługi paginacji, filtrowania czy sortowania w kontrolerze — metoda GetAll zwraca całą listę w pamięci.
- Brak mechanizmu trwałego przechowywania; dane są trzymane w statycznej liście w pamięci procesu (przykładowe dane zainicjalizowane w kodzie).
- Brak walidacji wejścia (nie widać atrybutów walidacyjnych ani sprawdzania poprawności danych).

## Historia zmian
- 2026-09-03 — Utworzono dokumentację na podstawie kodu źródłowego (aktualizacja automatyczna).

---
Generowane automatycznie z repozytorium: ppnet1980/ProductCatalogApi
