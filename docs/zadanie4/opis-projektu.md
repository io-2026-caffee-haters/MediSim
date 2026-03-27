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

** W przyszłości system może zostać rozszerzony o dziedziczenie klas badań (np. XRayTest) **
  
## I - Interface Segregation Principle (Zasada segregacji interfejsów):

* `Patient`: Mimo że klasa `Patient` posiada wiele metod, system separuje dostęp do nich. `UIManager` korzysta wyłącznie z metod tekstowych/wizualnych (jak `GetInterviewText`), podczas gdy `MedicalTest` widzi tylko logikę diagnostyczną (`GetSymptomsDetectableByTest`). Zapobiega to sytuacji, w której moduł diagnostyczny musiałby polegać na metodach renderowania grafiki.

* `DatabaseManager`: Klasa dostarcza specyficzne metody pobierania danych (np. GetDiseaseById, GetTestById) zamiast jednej ogólnej metody zwracającej surowy tekst. Pozwala to innym klasom, takim jak MenuManager czy GameManager, korzystać tylko z tych funkcjonalności bazy danych, których aktualnie potrzebują.

** W przyszłości po powiększeniu systemu możliwe byłoby wydzielenie interfejsów (np. diagnostycznych i wizualnych), aby jeszcze lepiej spełnić zasadę ISP **

## D - Dependency Inversion Principle (Zasada odwrócenia zależności):

* `MedicalTest (metoda Execute)`: Badanie nie tworzy własnych instancji pacjenta ani bazy danych, lecz otrzymuje je jako parametry (Patient patient, DatabaseManager db). Dzięki temu logika konkretnego testu zależy od abstrakcji/managera, a nie od konkretnych implementacji, co ułatwia testowanie jednostkowe w izolacji.

* `GameManager`: Główny manager gry nie zarządza bezpośrednio plikami na dysku, lecz komunikuje się z `DatabaseManager`. Wysokopoziomowa logika rozgrywki jest oddzielona od niskopoziomowych szczegółów zapisu/odczytu JSON, co pozwala na łatwą zmianę sposobu przechowywania danych w przyszłości.

# Wykorzystane Wzorce Projektowe
## 1. Fasada (Facade)

Wzorzec ten polega na udostępnieniu uproszczonego interfejsu do złożonego podsystemu.

* **Problem:** System posiada wiele niskopoziomowych modeli danych (Disease, Symptom, SaveData) oraz skomplikowany proces ładowania plików JSON. Gdyby każda klasa (np. MenuManager lub Patient) musiała sama zarządzać odczytem plików, kod byłby skrajnie trudny w utrzymaniu.

* **Rozwiązanie:** Stworzenie jednej klasy, która "przykrywa" złożoność zarządzania danymi i udostępnia proste metody dla reszty systemu.

* **Zastosowanie w MediSim:** Klasa DatabaseManager pełni rolę Fasady. Ukrywa ona przed resztą aplikacji (np. przed GameManager) szczegóły dotyczące ścieżek do plików, serializacji JSON czy zarządzania listami obiektów, oferując w zamian gotowe metody typu GetRandomDisease() czy SaveGameState().

* **Zgodność z SOLID:** Wspiera zasadę SRP (Single Responsibility) – tylko jedna klasa wie, jak fizycznie składowane są dane, podczas gdy inne klasy zajmują się wyłącznie logiką gry.

# 2. Strategia (Strategy) – w wersji uproszczonej (Data-Driven)

Wzorzec ten pozwala na definiowanie rodziny algorytmów i czynienie ich wymiennymi.

* **Problem:** Każda choroba w grze wymaga innego zestawu objawów i innej logiki wykrywania ich przez badania. Hardkodowanie logiki dla każdej choroby w klasie Patient uczyniłoby ją gigantyczną i niemożliwą do rozbudowy.

* **Rozwiązanie:** Wykorzystanie obiektów danych, które definiują zachowanie (tzw. Data-Driven Design), co w systemach gier jest uproszczoną formą wzorca Strategia.

* **Zastosowanie w MediSim:** Klasa MedicalTest wraz z metodą Execute() działa jak wymienna strategia diagnostyczna. Ponieważ GameManager przyjmuje dowolny obiekt MedicalTest z listy i wywołuje na nim tę samą metodę, algorytm badania zmienia się dynamicznie w zależności od tego, jaki test został wylosowany lub wybrany, bez zmiany kodu w GameManager.

* **Zgodność z SOLID:** Realizuje zasadę OCP (Open/Closed) – system jest otwarty na nowe "strategie" badawcze (poprzez dodanie ich do JSON), ale zamknięty na modyfikacje w silniku wykonawczym.
