# Wymagania dla dokumentacji Markdown

## Wymagane sekcje w dokumentacji Markdown

Dokumentacja ma składać się DOKŁADNIE z następujących sekcji (zabrania się tworzenia jakichkolwiek dodatkowych sekcji, takich jak "Braki", "Uwagi" czy "Zastrzeżenia"):

1. Tytuł dokumentu
2. Wersja API
3. Historia zmian
4. Krótki opis API
5. Lista endpointów (tabela zbiorcza)
6. Opis szczegółowy endpointów (szczegóły pod tabelą)

Dla każdego endpointu w sekcji szczegółowej należy zawrzeć wyłącznie:
   a) Parametry wejściowe
   b) Przykładowe requesty (JSON)
   c) Przykładowe response'y (JSON)
   d) Kody odpowiedzi HTTP

## Wymagania jakościowe dla AI

Model AI:
- ma opierać się wyłącznie na dostarczonych plikach i diffie,
- nie może wymyślać endpointów ani parametrów,
- ma zachować spójny format Markdown,
- NIE MOŻE dodawać sekcji "Braki", "Brakujące elementy" ani żądnych uwag technicznych do generowanego pliku,
- dokumentacja nie może zawierać poleceń cURL.
- KATEGORYCZNY ZAKAZ GENEROWANIA NAGŁÓWKÓW I SEKCJI TAKICH JAK:
   - ## Uruchomienie lokalne / uwagi implementacyjne
   - ## Braki zgodnie z DocumentationRequirements.md
   - ## Proponowane następne kroki (zalecenia)
   - ## Uwagi
   - ## Ograniczenia
