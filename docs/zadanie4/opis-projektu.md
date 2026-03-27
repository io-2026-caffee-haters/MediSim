# Zastosowanie zasady SOLID

## S - Single Responsibility Principle (Zasada jednej odpowiedzialności):

* `UIManager`: Skupia się na prezentacji danych graczowi i obsłudze zdarzeń interfejsu.

* `Patient`: Przechowuje stan kliniczny pacjenta i zarządza logiką powiązania objawów z chorobą.

* `DatabaseManager`: Odpowiada wyłącznie za niskopoziomowe operacje wejścia/wyjścia (JSON) oraz dostarczanie surowych danych do systemu.

* `Disease`: Przechowuje opis choroby.

* `MedicalTest`: Definiuje mechanikę badania i parametry jak cooldown, koszt czasowy.

## O - Open/Closed Principle (Zasada otwarte-zamknięte):

* `MedicalTest`: Klasa jest "otwarta" na nowe rodzaje badań. Można dodać do gry dowolną liczbę nowych testów (np. USG, EKG) poprzez tworzenie nowych instancji w plikach JSON.

* `Patient`: Jest otwarty na obsługę nowej choroby i nowego badania. Mechanizm `Initialize(disease)` pozwala pacjentowi przyjąć dowolny zestaw danych medycznych, a metoda `GetSymptomsDetectableByTest` uniwersalnie współpracuje z każdym nowym badaniem bez modyfikacji kodu klasy Patient.

* `Disease`: Stanowi otwarty szablon dla bazy wiedzy. System jest zamknięty na zmiany w strukturze kodu, ale otwarty na rozszerzanie listy chorób poprzez zewnętrzne pliki.

## L - Liskov Substitution Principle (Zasada podstawienia Liskov):

* `MedicalTest`: Klasa bazowa definiuje wspólny kontrakt dla wszystkich badań poprzez metodę `Execute()`. Dzięki temu `GameManager` może wywoływać tę metodę na dowolnym obiekcie typu `MedicalTest`, mając gwarancję, że każde specyficzne badanie zwróci wynik w oczekiwanym formacie bez przerywania działania gry.

* `Symptom`: Każdy obiekt symptomu, niezależnie od tego, czy jest widoczny na zewnątrz (nakładka), czy ukryty (tylko wywiad), jest traktowany przez pacjenta w ten sam sposób podczas sprawdzania wykrywalności. Pozwala to na swobodne zastępowanie jednych objawów innymi w definicji choroby bez zmiany logiki ich przetwarzania.
  
## I - Interface Segregation Principle (Zasada segregacji interfejsów):

* `Patient`: Mimo że klasa `Patient` posiada wiele metod, system separuje dostęp do nich. `UIManager` korzysta wyłącznie z metod tekstowych/wizualnych (jak `GetInterviewText`), podczas gdy `MedicalTest` widzi tylko logikę diagnostyczną (`GetSymptomsDetectableByTest`). Zapobiega to sytuacji, w której moduł diagnostyczny musiałby polegać na metodach renderowania grafiki.

* `DatabaseManager`: Klasa dostarcza specyficzne metody pobierania danych (np. GetDiseaseById, GetTestById) zamiast jednej ogólnej metody zwracającej surowy tekst. Pozwala to innym klasom, takim jak MenuManager czy GameManager, korzystać tylko z tych funkcjonalności bazy danych, których aktualnie potrzebują.

## D - Dependency Inversion Principle (Zasada odwrócenia zależności):

* `MedicalTest (metoda Execute)`: Badanie nie tworzy własnych instancji pacjenta ani bazy danych, lecz otrzymuje je jako parametry (Patient patient, DatabaseManager db). Dzięki temu logika konkretnego testu zależy od abstrakcji/managera, a nie od konkretnych implementacji, co ułatwia testowanie jednostkowe w izolacji.

* `GameManager`: Główny manager gry nie zarządza bezpośrednio plikami na dysku, lecz komunikuje się z `DatabaseManager`. Wysokopoziomowa logika rozgrywki jest oddzielona od niskopoziomowych szczegółów zapisu/odczytu JSON, co pozwala na łatwą zmianę sposobu przechowywania danych w przyszłości.

# Wykorzystane Wzorce Projektowe
## 1. Obserwator (Observer)
## 2. Stan (State)
