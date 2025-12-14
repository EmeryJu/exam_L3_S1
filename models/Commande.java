package models;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

public class Commande {
    private int id;
    private int client;
    private List<Burger> burgers = new ArrayList<>();
    private List<Menu> menus = new ArrayList<>();
    private List<Complement> complements = new ArrayList<>();
    private EtatCommande etat = EtatCommande.EN_COURS;
    private TypeCommande type;
    private LocalDate date = LocalDate.now();
    private boolean payee = false;

    public Commande(int id, int client, TypeCommande type) {
        this.id = id;
        this.client = client;
        this.type = type;
    }

    public double calculerTotal() {
        double total = 0;
        for (Burger b : burgers) total += b.getPrix();
        for (Menu m : menus)total += m.getPrix();
        for (Complement c : complements) total += c.getPrix();
        return total;
    }

    public void payer() {
        if (!payee) {
            payee = true;
            etat = EtatCommande.VALIDE;
        }
    }

    public EtatCommande getEtat() { return etat; }
    public LocalDate getDate() { return date; }
}