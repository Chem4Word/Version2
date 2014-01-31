function ShowMol(mol, length) {
    // Remove Explict Hydrogens
    var ded = new ChemDoodle.informatics.HydrogenDeducer();
    ded.removeHydrogens(mol);
    // Scale and display
    var len = parseInt(length);
    mol.scaleToAverageBondLength(len);
    sketcher.loadMolecule(mol);
}

function SetMolFile(molFile, length) {
    var mol = ChemDoodle.readMOL(molFile);
    ShowMol(mol, length);
}

function SetJSON(molFile, length) {
    var jsonMol = JSON.parse(molFile);
    var mol = new ChemDoodle.io.JSONInterpreter().molFrom(jsonMol);
    ShowMol(mol, length);
}

function ReScale(length) {
    var mol = sketcher.getMolecule();
    var len = parseInt(length);
    mol.scaleToAverageBondLength(len);
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function GetMolFile() {
    var mol = sketcher.getMolecule();
    var molFile = new ChemDoodle.io.MOLInterpreter().write(mol);
    return molFile;
}

function GetJSON() {
    var mol = sketcher.getMolecule();
    // Re-Scale to 1.54 for Chem4Word rendering
    mol.scaleToAverageBondLength(1.54);
    var jsonMol = new ChemDoodle.io.JSONInterpreter().molTo(mol);
    var asString = JSON.stringify(jsonMol);
    return asString;
}

function GetFormula() {

    function compare(a, b) {
        if (a.label < b.label)
            return -1;
        if (a.label > b.label)
            return 1;
        return 0;
    }

    // Allow for Hill Notation CH first, then alphabetical
    var fatoms = [];
    fatoms.push({ label: 'C', freq: 0 });
    fatoms.push({ label: 'H', freq: 0 });

    var mol = sketcher.getMolecule();
    var atoms = mol.atoms;
    for (var i = 0, ii = atoms.length; i < ii; i++) {
        var found = false;
        for (var j = 0, jj = fatoms.length; j < jj; j++) {
            if (fatoms[j].label === atoms[i].label) {
                fatoms[j].freq += 1;
                found = true;
                break;
            }
        }
        if (!found) {
            fatoms.push({ label: atoms[i].label, freq: 1 });
        }
        // Second element is Hydrogen count
        fatoms[1].freq += atoms[i].getImplicitHydrogenCount();
    }

    // Generate Formula in Hill Notation CH first, then alphabetical
    var result = "";
    var others = [];

    for (var k = 0, kk = fatoms.length; k < kk; k++) {
        if (k < 2) {
            if (fatoms[k].freq > 0) {
                result += fatoms[k].label + " " + fatoms[k].freq + " ";
            }
        }
        else {
            others.push(fatoms[k]);
        }
    }

    others.sort(compare);
    for (var l = 0, ll = others.length; l < ll; l++) {
        result += others[l].label + " " + others[l].freq + " ";
    }

    return result.trim();
}

function GetVersion() {
    return ChemDoodle.getVersion();
}