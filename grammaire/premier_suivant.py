import re 
import copy
import json 

def ite_P_eps(Regles_eps,P):
    copy_P = copy.deepcopy(P)
    for r in Regles_eps:
        if r[0] not in copy_P:
            i = 2
            v = True
            while i<len(r) and v:
                if r[i] not in copy_P:
                    v = False
                else :
                    i += 1
            if v:
                copy_P.append(r[0])
    return copy_P

def calcul_P_eps(Regles):
    #on éléimine les regles contenant des terminaux car elles ne peuvent donner le mot vide
    Regles_eps = []
    for r in Regles:
        content_eps = True 
        for i in Terminaux:
            if i in r:
                content_eps = False
                break
            
        if content_eps:
            Regles_eps.append(r)
    
    #determination de P_eps
    P_eps_current = []
    P_eps_new = ite_P_eps(Regles_eps,P_eps_current)
    while P_eps_current != P_eps_new:
        P_eps_current = P_eps_new
        P_eps_new = ite_P_eps(Regles_eps,P_eps_current)
    
                        
    return P_eps_current

def ajoute_premier_a_premier(P,nt1,nt2):
    #on ajoute Premier(nt1) à Premier(nt2)
    i_nt1 = Non_Terminaux.index(nt1)
    i_nt2 = Non_Terminaux.index(nt2)
    for p in P[i_nt1]:
        if p not in P[i_nt2]:
            P[i_nt2].append(p)
    
def ite_Premier(P):
    Copy_P = copy.deepcopy(P)
    for r in Regles:
        if len(r)>2:
            if r[2] in Terminaux:
                if r[2] not in Copy_P[Non_Terminaux.index(r[0])]:
                    Copy_P[Non_Terminaux.index(r[0])].append(r[2])
            else:
                ajoute_premier_a_premier(Copy_P,r[2],r[0])
                i=3
                while i<len(r) and r[i-1] in P_eps:
                    if r[i] in Terminaux:
                        if r[i] not in Copy_P[Non_Terminaux.index(r[0])]:
                            Copy_P[Non_Terminaux.index(r[0])].append(r[i])
                            break
                    else:    
                        ajoute_premier_a_premier(Copy_P,r[i],r[0])
                    i+=i
    return Copy_P
                


def calcul_premiers():
    Premier_current = [[] for i in Non_Terminaux]
    Premier_new = ite_Premier(Premier_current)
    while Premier_current != Premier_new:
        Premier_current = Premier_new
        Premier_new = ite_Premier(Premier_current)
    return Premier_current 

def ajoute_premier_a_suivant(S,r,i):
    i_B = Non_Terminaux.index(r[i])
    i += 1 
    b = True
    while i<len(r) and b:
        if r[i] in Terminaux:
            if r[i] not in S[i_B]:
                S[i_B].append(r[i])
            b = False
        else:
            for p in Premiers[Non_Terminaux.index(r[i])]:
                if p not in S[i_B]:
                    S[i_B].append(p)
            if r[i] not in P_eps:
                b = False 
        i += 1
        
def ajoute_suivant_a_suivant(S,nt1,nt2):
    # On ajoute suivant(nt1) à suivant(nt2)
    i_nt1 = Non_Terminaux.index(nt1)
    i_nt2 = Non_Terminaux.index(nt2)
    for s in S[i_nt1]:
        if s not in S[i_nt2]:
            S[i_nt2].append(s)

   
def ite_suivant(S):
    Copy_S = copy.deepcopy(S)
    for r in Regles:
        i = len(r)-1
        if r[i] in Non_Terminaux:
            ajoute_suivant_a_suivant(Copy_S, r[0], r[i])
        i -= 1
        while i>1 and r[i+1] in P_eps:
            if r[i] in Non_Terminaux:
                ajoute_suivant_a_suivant(Copy_S, r[0], r[i])
            i -= 1
    return Copy_S

                
        
                
  

def calcul_suivants():
    Suivant_current = [[] for i in Non_Terminaux]
    Suivant_current[0].append('$')
    
    #pour chaque règle de la forme A → αBβ où B ∈ N ajouter Premier(β) à Suivant(B)
    for r in Regles:
        for i in range(2,len(r)-1):
            if r[i] in Non_Terminaux :
                ajoute_premier_a_suivant(Suivant_current,r,i)
                #i est tel que r[i] = B
                
    Suivant_new = ite_suivant(Suivant_current)
    while Suivant_current != Suivant_new:
        Suivant_current = Suivant_new
        Suivant_new = ite_suivant(Suivant_current)          
    return Suivant_current
                   
    



# ATTENTION : La première règle du fichier.txt doit absolument être l'axiome !
grammaire = open("LL1.txt", "r")

Regles = grammaire.readlines()

for i in Regles:
    if i == '\n':
        Regles.remove(i)


for i in range(0,len(Regles)):
    Regles[i] = re.sub(r"\n", "", Regles[i])
 

for i in range(0,len(Regles)):
    Regles[i] = Regles[i].split(" ")  
    
for i in range(0,len(Regles)):
    for j in Regles[i]:
        if j == "":
            Regles[i].remove(j)
            
   
Non_Terminaux = []

Terminaux = []

for r in Regles:
    if r[0] not in Non_Terminaux:
        Non_Terminaux.append(r[0])

for r in Regles:
    for i in range(2,len(r)):
        if r[i] not in Non_Terminaux and r[i] not in Terminaux:
            Terminaux.append(r[i])
            
print(Non_Terminaux)

print(Terminaux)

        

P_eps = calcul_P_eps(Regles)
                
Premiers = calcul_premiers()

Suivants = calcul_suivants()

grammaire.close()



with open("premier_suivant.json", "w") as f: 
	json.dump([Non_Terminaux,Terminaux,P_eps,Premiers,Suivants],f) 
 

    



