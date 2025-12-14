package models;
public class Menu {
    private int id;
    private String name;
    private String iamge;
    private Burger burger;
    private Complement boisson;
    private Complement frites;

    public Menu(int id, String name, String iamge, Burger burger, Complement boisson, Complement frites) {
        this.id = id;
        this.name = name;
        this.iamge = iamge;
        this.burger = burger;
        this.boisson = boisson;
        this.frites = frites;
    }

    public double getPrix() {
        return burger.getPrix() + boisson.getPrix() + frites.getPrix();
    }

    public String getNom() { return nom; }
}