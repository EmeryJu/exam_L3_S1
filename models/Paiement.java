package models;

public class Paiement {
    private LocalDate date;
    private double montant;
    private ModePaiement mode;

    public Paiement(double montant, ModePaiement mode) {
        this.date = LocalDate.now();
        this.montant = montant;
        this.mode = mode;
    }
}
