package services;

import java.util.ArrayList;
import java.util.List;
import models.Paiement;
import models.Commande;
import enums.ModePaiement;

public class PaiementService {
    private List<Paiement> paiements = new ArrayList<>();
    private int nextId = 1;

    public boolean traiterPaiement(Commande commande, ModePaiement mode) {
        double montant = commande.calculerTotal();
        Paiement paiement = new Paiement(nextId++, montant, mode);
        paiements.add(paiement);
        // Simulate payment processing
        boolean success = true; // Assume success for now
        if (success) {
            commande.payer();
        }
        return success;
    }

    public void enregistrerPaiement(Paiement paiement) {
        paiements.add(paiement);
    }

    public List<Paiement> listerTous() {
        return new ArrayList<>(paiements);
    }
}
