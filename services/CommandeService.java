package services;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

import models.Commande;

public class CommandeService {
    private List<Commande> commandes = new ArrayList<>();

    public void creerCommande(Commande commande) {
        commandes.add(commande);
}
    public void annuler(int Id) {
        for (Commande c : commandes) {
            if (c.getDate().equals(LocalDate.now())) {
                c.getEtat();
            }
        }
    }

public List<Commande> listerCommandesJour() {
        List<Commande> commandesJour = new ArrayList<>();
        for (Commande c : commandes) {
            if (c.getDate().equals(LocalDate.now())) {
                commandesJour.add(c);
            }
        }
        return commandesJour;
    }
}
