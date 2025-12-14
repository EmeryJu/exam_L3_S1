public class Main {
    public static void main(String[] args) {
        Burger burger = new Burger(1, "Cheeseburger", 5.99, "cheeseburger.png");
        Complement boisson = new Complement(1, "Coca-Cola", 1.99, "coca_cola.png");
        Complement frites = new Complement(2, "Fries", 2.49, "fries.png");

        Menu menu = new Menu(1, "Menu 1", "menu1.png", burger, boisson, frites);

        client client = new Client(1, "Mamadou", "Diop", "774567890", "ali", "1234");

        Commande commande = new Commande(1, client, TypeCommande.LIVRAISON);
        commande.ajouterMenu(menu);
        commande.payer();

        System.out.println("Total de la commande: " + commande.calculerTotal());
        System.out.println("État de la commande: " + commande.getEtat());
        }
}
