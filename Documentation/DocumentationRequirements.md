# Wymagania dla dokumentacji Markdown

## Cel dokumentu

Ten plik definiuje wymagania dla dokumentacji technicznej API.

## Wejścia wymagane dla AI

Model powinien otrzymać:
- ścieżkę do istniejącego pliku dokumentacji w folderze `Documentation`, jeśli istnieje
- ścieżkę do tego pliku z wymaganiami
- listę zmienionych plików źródłowych
- pełną treść plików kontrolera i modeli albo diff zmian
- nazwę API i wersję

## Wymagane sekcje w dokumentacji Markdown

Dokumentacja ma zawierać dokładnie 8 sekcji:

1. Tytuł dokumentu
2. Wersję API
3. Historię zmian
4. Krótki opis API
6. Listę endpointów w postaci tabeli
7. Opis każdego endpointu w tej samej tabeli
8. Szczegóły endpointu już pod tabelą zawierające
 
    a) Parametry wejściowe
   
    b) Przykładowe requesty
   
    c) Przykładowe response'y
   
    d) Kody odpowiedzi HTTP


## Wymagania dla opisu endpointów

Dla każdego endpointu należy opisać:

- metodę HTTP
- ścieżkę
- cel biznesowy endpointu
- body requestu, jeśli występuje
- strukturę odpowiedzi
- możliwe statusy HTTP

## Wymagania jakościowe dla AI

Model AI:
- ma opierać się wyłącznie na dostarczonych plikach i diffie
- nie może wymyślać endpointów ani parametrów
- ma zachować spójny format Markdown
- ma zawierać dokładnie 8 sekcji
- dokumentacja ma nie zawierać ani curl ani brakujących elementów


## Wymagania dla historii zmian

Sekcja historii zmian powinna zawierać:

- datę wygenerowania lub aktualizacji
- autora lub nazwę procesu
- krótki opis zakresu zmian

## Wymagany układ Markdown

## Tytuł
## Historia zmian
## Opis API
## Endpointy



