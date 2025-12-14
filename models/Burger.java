package models;

public class Burger {
    private int id;
    private String nom;
    private double prix;
    private String image;
    private boolean archive = false;

    public Burger(int id, String nom, double prix, String image) {
        this.id = id;
        this.nom = nom;
        this.prix = prix;
        this.image = image;
    }
    public int getID() { return id; }
    public String getNom() { return nom; }
    public double getPrix() { return prix; }
    public boolean isArchive() { return archive; }

public void setNom(String nom) { this.nom = nom; }
    public void setPrix(double prix) { this.prix = prix; }
    public void archiver() { this.archive = true; }
}