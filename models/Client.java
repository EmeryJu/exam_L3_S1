package models;

public class Client {
    private int id;
    private String nom;
    private String prenom;
    private String telephone;
    private String login;
    private String password;

    public Client(int id, String nom, String prenom, String telephone, String login, String password) {
        this.id = id;
        this.nom = nom;
        this.prenom = prenom;
        this.telephone = telephone;
        this.login = login;
        this.password = password;
    }

    public String getLogin() { return login; }
    public String getPassword() { return password; }
}
