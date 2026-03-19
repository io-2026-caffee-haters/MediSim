flowchart LR

    Gracz(("👤 Gracz"))
    System(("⚙️ System / Baza JSON"))

    subgraph MediSim [Aplikacja MediSim]
        direction TB
        U1(["U-01: Przyjęcie pacjenta"])
        U2(["U-02: Przeprowadzenie wywiadu"])
        U3(["U-03: Zlecenie badania dodatkowego"])
        U4(["U-04: Korzystanie z dziennika medycznego"])
        U4a(["U-04a*: Dodanie własnej notatki"])
        U5(["U-05: Postawienie diagnozy"])
        U6(["U-06*: Zapisanie stanu gry"])
        U7(["U-07*: Wczytanie stanu gry"])
    end

    Gracz --- U1
    Gracz --- U2
    Gracz --- U3
    Gracz --- U4
    Gracz --- U4a
    Gracz --- U5
    Gracz --- U6
    Gracz --- U7

    U1 --- System
    U2 --- System
    U3 --- System
    U4 --- System
    U4a --- System
    U5 --- System
    U6 --- System
    U7 --- System
