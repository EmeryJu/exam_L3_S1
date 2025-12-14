package models;

public class Complement {
    private int id;
    private String nome;
    private double prix;
    private String image;
    private boolean archive = false;

    public Complement(int id, String nome, double prix, String image) {
        this.id = id;
        this.nome = nome;
        this.prix = prix;
        this.image = image;
    }

    public double getPrix() { return prix; }
    public String getNom() { return nom; }
    public boolean isArchive() { return archive; }

    public void archiver() { this.archive = true; }
}