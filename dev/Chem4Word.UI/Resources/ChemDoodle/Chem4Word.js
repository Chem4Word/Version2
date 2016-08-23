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
    var i;
    var j;
    var jj;

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

        for (i = 0; i < hToAdd; i++) {
            var hAtomAngle = Math.radians(atomDegrees);
            var newAtomX = atom.x + (bondlength * Math.cos(hAtomAngle));
            var newAtomY = atom.y - (bondlength * Math.sin(hAtomAngle));
            // Detect and bump any overlapping atoms using a hit area 10% of average bond length
            for (j = 0, jj = atoms.length; j < jj; j++) {
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

function GetAverageBondLength() {
    var mol = sketcher.getMolecule();
    var avBondLength = mol.getAverageBondLength();
    return avBondLength;
}

function AddExplicitHydrogens() {
    var mol = sketcher.getMolecule();

    var i;
    var ii;

    var avBondLength = mol.getAverageBondLength();
    if (avBondLength == 0) {
        avBondLength = 20;
    }

    atoms = mol.atoms;
    bonds = mol.bonds;
    rings = mol.rings;
    atomIdx = atoms.length;
    bondIdx = bonds.length;

    for (i = 0, ii = atoms.length; i < ii; i++) {
        AddHydrogensToAtom(atoms, bonds, avBondLength, atoms[i]);
    }

    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function RemoveHydrogens() {
    var mol = sketcher.getMolecule();
    var atoms = [];
    var bonds = [];

    var i;
    var ii;
    var j;
    var jj;

    for (i = 0, ii = mol.bonds.length; i < ii; i++) {
        if (mol.bonds[i].a1.label !== 'H' && mol.bonds[i].a2.label !== 'H') {
            bonds.push(mol.bonds[i]);
        }
        else if (mol.bonds[i].stereo !== 'none') {
            bonds.push(mol.bonds[i]);
        }
    }

    for (i = 0, ii = mol.atoms.length; i < ii; i++) {
        if (mol.atoms[i].label !== 'H') {
            atoms.push(mol.atoms[i]);
        }
        else {
            for (j = 0, jj = mol.atoms[i].bonds.length; j < jj; j++) {
                if (mol.atoms[i].bonds[j].stereo !== 'none') {
                    atoms.push(mol.atoms[i]);
                    break;
                }
            }
        }
    }

    mol.atoms = atoms;
    mol.bonds = bonds;

    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function InitialiseSketcherOptions(showAtomsInColour, showImplicitHydrogens) {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_useJMOLColors = showAtomsInColour;
    sketcher.specs.atoms_implicitHydrogens_2D = showImplicitHydrogens;
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function AtomsInColour(value) {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_useJMOLColors = value;
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function HideImplicitHCount() {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_implicitHydrogens_2D = false;
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function ShowImplicitHCount() {
    var mol = sketcher.getMolecule();
    sketcher.specs.atoms_implicitHydrogens_2D = true;
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function ShowMol(mol) {
    sketcher.loadMolecule(mol);
    sketcher.center();
    sketcher.repaint();
}

function SetMolFile(molFile, length) {
    var mol = ChemDoodle.readMOL(molFile);
    ShowMol(mol, length);
}

function SetJSON(molFile) {
    var jsonMol = JSON.parse(molFile);
    var mol = new ChemDoodle.io.JSONInterpreter().molFrom(jsonMol);
    ShowMol(mol);
}

function ReScale(length) {
    var mol = sketcher.getMolecule();
    var len = parseInt(length);
    mol.scaleToAverageBondLength(len);
    sketcher.specs.bondLength_2D = len;
    ShowMol(mol);
}

function GetMolFile() {
    var mol = sketcher.getMolecule();
    var molFile = new ChemDoodle.io.MOLInterpreter().write(mol);
    return molFile;
}

function GetJSON() {
    var mol = sketcher.getMolecule();
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