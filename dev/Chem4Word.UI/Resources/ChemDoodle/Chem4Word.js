// Converts from degrees to radians.
Math.radians = function (degrees) {
    return degrees * Math.PI / 180;
};

// Converts from radians to degrees.
Math.degrees = function (radians) {
    return radians * 180 / Math.PI;
};

function AddHydrogensToAtom(atoms, bonds, bondlength, atom) {
    var atomIdx = atoms.length;
    var bondIdx = bonds.length;

    var hToAdd = atom.getImplicitHydrogenCount();

    if (hToAdd > 0) {
        var seperation = 90;
        if (atom.bonds.length > 1) {
            seperation = 30;
        }

        var atomAngle = atom.angleOfLeastInterference;

        var atomDegrees = Math.degrees(atomAngle);
        switch (hToAdd) {
            case 1:
                atomDegrees = atomDegrees;
                break;
            case 2:
                atomDegrees = atomDegrees + (0.5 * seperation);
                break;
            case 3:
                atomDegrees = atomDegrees + (1.0 * seperation);
                break;
        }

        for (var i = 0; i < hToAdd; i++) {
            var hAtomAngle = Math.radians(atomDegrees);
            var newAtomX = atom.x + (bondlength * Math.cos(hAtomAngle));
            var newAtomY = atom.y - (bondlength * Math.sin(hAtomAngle));
            // Detect and bump any overlapping atoms using a hit area 10% of average bond length
            for (var j = 0, jj = atoms.length; j < jj; j++) {
                var xdelta = Math.abs(atoms[j].x - newAtomX);
                var ydelta = Math.abs(atoms[j].y - newAtomY);
                //var xdelta = Math.abs(Math.floor(atoms[j].x) - Math.floor(newAtomX));
                //var ydelta = Math.abs(Math.floor(atoms[j].y) - Math.floor(newAtomY));
                if (xdelta <= (bondlength / 10) && ydelta <= (bondlength / 10)) {
                    hAtomAngle = Math.radians(atomDegrees - 45);
                    newAtomX = atom.x + ((bondlength / 2) * Math.cos(hAtomAngle));
                    newAtomY = atom.y - ((bondlength / 2) * Math.sin(hAtomAngle));
                    break;
                }
            }
            var hydrogen = new ChemDoodle.structures.Atom('H');
            hydrogen.x = atom.x + (bondlength * Math.cos(hAtomAngle));
            hydrogen.y = atom.y - (bondlength * Math.sin(hAtomAngle));
            var bond = new ChemDoodle.structures.Bond(atom, hydrogen, 1);
            atoms[atomIdx++] = hydrogen;
            bonds[bondIdx++] = bond;
            atomDegrees -= seperation;
        }
    }
}

function AddExplicitHydrogens() {
    var mol = sketcher.getMolecule();

    var avBondLength = mol.getAverageBondLength();
    if (avBondLength == 0) {
        avBondLength = 20;
    }

    atoms = mol.atoms;
    bonds = mol.bonds;
    rings = mol.rings;
    atomIdx = atoms.length;
    bondIdx = bonds.length;

    for (var i = 0, ii = atoms.length; i < ii; i++) {
        AddHydrogensToAtom(atoms, bonds, avBondLength, atoms[i]);
    }

    sketcher.loadMolecule(mol);
    sketcher.center();
}

function RemoveHydrogens() {
    var mol = sketcher.getMolecule();
    var ded = new ChemDoodle.informatics.HydrogenDeducer();
    ded.removeHydrogens(mol);
    sketcher.loadMolecule(mol);
    sketcher.center();
}

function HideImplicitHCount() {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_implicitHydrogens_2D = false;
    sketcher.loadMolecule(mol);
    sketcher.center();
}

function ShowImplicitHCount() {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_implicitHydrogens_2D = true;
    sketcher.loadMolecule(mol);
    sketcher.center();
}

function ShowMol(mol, length) {
    // Remove Explict Hydrogens
    // var ded = new ChemDoodle.informatics.HydrogenDeducer();
    // ded.removeHydrogens(mol);
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