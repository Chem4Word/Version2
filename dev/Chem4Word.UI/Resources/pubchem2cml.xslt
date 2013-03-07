<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0" xmlns:pc="http://www.ncbi.nlm.nih.gov" xmlns:cml="http://www.xml-cml.org/schema">

  <xsl:variable name="compound" select="/pc:PC-Compound" />
  <xsl:variable name="id" select="$compound/pc:PC-Compound_id" />
  <xsl:variable name="compoundAtoms" select="$compound/pc:PC-Compound_atoms" />
  <xsl:variable name="bonds" select="$compound/pc:PC-Compound_bonds" />
  <xsl:variable name="coords" select="$compound/pc:PC-Compound_coords" />
  <xsl:variable name="conformers" select="$coords/pc:PC-Coordinates/pc:PC-Coordinates_conformers/pc:PC-Conformer" />
  <xsl:variable name="charge" select="$compound/pc:PC-Compound_charge" />
  <xsl:variable name="props" select="$compound/pc:PC-Compound_props" />
  <xsl:variable name="count" select="$compound/pc:PC-Compound_count" />
  <xsl:template match="/">
    <cml:cml convention="cmlDict:cmllite" xmlns:cmlDict="http://www.xml-cml.org/dictionary/cml/" xmlns:bondDict="http://www.xml-cml.org/dictionary/bond/">
      <cml:molecule formalCharge="{$charge}" id="m1">
        <xsl:apply-templates select="$compound" />
      </cml:molecule>
    </cml:cml>
  </xsl:template>

  <xsl:template match="pc:PC-Compound">
    <xsl:apply-templates select="$id" />
    <xsl:apply-templates select="$compoundAtoms" />
    <xsl:apply-templates select="$bonds" />
    <xsl:apply-templates select="$props" />
  </xsl:template>

  <xsl:template match="pc:PC-Compound_id">
    <cml:name dictRef="pc:CID">
      <xsl:value-of select="normalize-space(.)" />
    </cml:name>
  </xsl:template>
  <!-- 
   <PC-Compound_atoms>
- <PC-Atoms>
- <PC-Atoms_aid>
  <PC-Atoms_aid_E>1</PC-Atoms_aid_E> 
  <PC-Atoms_aid_E>2</PC-Atoms_aid_E> 
  <PC-Atoms_aid_E>3</PC-Atoms_aid_E> 
  </PC-Atoms_aid>
- <PC-Atoms_element>
  <PC-Element value="o">8</PC-Element> 
  <PC-Element value="h">1</PC-Element> 
  <PC-Element value="h">1</PC-Element> 
  </PC-Atoms_element>
+ <PC-Atoms_isotope>
- <PC-AtomInt>
  <PC-AtomInt_aid>2</PC-AtomInt_aid> 
  <PC-AtomInt_value>2</PC-AtomInt_value> 
  </PC-AtomInt>
- <PC-AtomInt>
  <PC-AtomInt_aid>3</PC-AtomInt_aid> 
  <PC-AtomInt_value>2</PC-AtomInt_value> 
  </PC-AtomInt>
  </PC-Atoms_isotope>
  </PC-Atoms>
  </PC-Compound_atoms>
   -->
  <xsl:template match="pc:PC-Compound_atoms">
    <xsl:variable name="atoms" select="pc:PC-Atoms" />
    <xsl:variable name="atomsAid" select="$atoms/pc:PC-Atoms_aid/pc:PC-Atoms_aid_E" />
    <xsl:variable name="atomsElement" select="$atoms/pc:PC-Atoms_element/pc:PC-Element" />
    <xsl:variable name="x" select="$conformers/pc:PC-Conformer_x/pc:PC-Conformer_x_E" />
    <xsl:variable name="y" select="$conformers/pc:PC-Conformer_y/pc:PC-Conformer_y_E" />
    <cml:atomArray>
      <xsl:for-each select="$atomsAid">
        <xsl:variable name="pos" select="position()" />
        <xsl:variable name="element" select="$atomsElement[$pos]/@value" />
        <xsl:variable name="elementOK" select="concat(translate(substring($element, 1, 1), 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'), substring($element, 2))" />

        <cml:atom id="a{$pos}" x2="{$x[$pos]}" y2="{$y[$pos]}" elementType="{$elementOK}"></cml:atom>
      </xsl:for-each>
    </cml:atomArray>
  </xsl:template>


  <xsl:template match="pc:PC-Compound_bonds">
    <xsl:variable name="bonds" select="pc:PC-Bonds" />
    <xsl:variable name="bondsAid1" select="$bonds/pc:PC-Bonds_aid1/pc:PC-Bonds_aid1_E" />
    <xsl:variable name="bondsAid2" select="$bonds/pc:PC-Bonds_aid2/pc:PC-Bonds_aid2_E" />
    <xsl:variable name="orders" select="$bonds/pc:PC-Bonds_order/pc:PC-BondType/@value" />
    <cml:bondArray>
      <xsl:for-each select="$bondsAid1">
        <xsl:variable name="pos" select="position()" />
        <xsl:variable name="aid1" select="." />
        <xsl:variable name="aid2" select="$bondsAid2[$pos]" />
        <xsl:variable name="order" select="$orders[$pos]" />
        <!--<xsl:variable name="orderCml" select="translate($order, '123', 'SDT')"/>-->
        <xsl:variable name="orderCml">
          <xsl:choose>
            <xsl:when test="$order = 'single'">S</xsl:when>
            <xsl:when test="$order = 'double'">D</xsl:when>
            <xsl:when test="$order = 'triple'">T</xsl:when>
            <xsl:when test="$order = 'quadruple'">other</xsl:when>
            <xsl:when test="$order = 'dative'">other</xsl:when>
            <xsl:when test="$order = 'complex'">other</xsl:when>
            <xsl:when test="$order = 'ionic'">other</xsl:when>
            <xsl:when test="$order = 'unknown'">unknown</xsl:when>
          </xsl:choose>
        </xsl:variable>
        <cml:bond id="a{$aid1}_a{$aid2}" order="{$orderCml}" atomRefs2="a{$aid1} a{$aid2}">
          <xsl:if test="$orderCml = 'other'">
            <xsl:attribute name="dictRef">
              <xsl:choose>
                <xsl:when test="$order = 'quadruple'">bondDict:quadruple</xsl:when>
                <xsl:when test="$order = 'dative'">bondDict:dative</xsl:when>
                <xsl:when test="$order = 'complex'">bondDict:complex</xsl:when>
                <xsl:when test="$order = 'ionic'">bondDict:ionic</xsl:when>
              </xsl:choose>
            </xsl:attribute>
            <xsl:attribute name="source" namespace="http://purl.org/dc/terms">
              ftp://ftp.ncbi.nih.gov/pubchem/specifications/pubchem.xsd
            </xsl:attribute>
          </xsl:if>
        </cml:bond>
      </xsl:for-each>
    </cml:bondArray>

  </xsl:template>

  <!-- 
  <PC-Compound_coords>
- <PC-Coordinates>
- <PC-Coordinates_type>
  <PC-CoordinateType value="twod">1</PC-CoordinateType> 
  <PC-CoordinateType value="computed">5</PC-CoordinateType> 
  <PC-CoordinateType value="units-unknown">255</PC-CoordinateType> 
  </PC-Coordinates_type>
- <PC-Coordinates_aid>
  <PC-Coordinates_aid_E>1</PC-Coordinates_aid_E> 
  <PC-Coordinates_aid_E>2</PC-Coordinates_aid_E> 
  <PC-Coordinates_aid_E>3</PC-Coordinates_aid_E> 
  </PC-Coordinates_aid>
- <PC-Coordinates_conformers>
- <PC-Conformer>
- <PC-Conformer_x>
  <PC-Conformer_x_E>2.5369</PC-Conformer_x_E> 
  <PC-Conformer_x_E>3.0739</PC-Conformer_x_E> 
  <PC-Conformer_x_E>2</PC-Conformer_x_E> 
  </PC-Conformer_x>
- <PC-Conformer_y>
  <PC-Conformer_y_E>-0.155</PC-Conformer_y_E> 
  <PC-Conformer_y_E>0.155</PC-Conformer_y_E> 
  <PC-Conformer_y_E>0.155</PC-Conformer_y_E> 
  </PC-Conformer_y>
  </PC-Conformer>
  </PC-Coordinates_conformers>
  </PC-Coordinates>
  </PC-Compound_coords>
   -->


  <!--
   <PC-InfoData>
- <PC-InfoData_urn>
- <PC-Urn>
  <PC-Urn_label>IUPAC Name</PC-Urn_label> 
  <PC-Urn_name>Allowed</PC-Urn_name> 
- <PC-Urn_datatype>
  <PC-UrnDataType value="string">1</PC-UrnDataType> 
  </PC-Urn_datatype>
  <PC-Urn_version>1.6.0</PC-Urn_version> 
  <PC-Urn_software>LexiChem</PC-Urn_software> 
  <PC-Urn_source>openeye.com</PC-Urn_source> 
  <PC-Urn_release>2007.09.04</PC-Urn_release> 
  </PC-Urn>
  </PC-InfoData_urn>
- <PC-InfoData_value>
  <PC-InfoData_value_sval>magnesium sulfate heptahydrate</PC-InfoData_value_sval> 
  </PC-InfoData_value>
  </PC-InfoData>
- <PC-InfoData>
     -->
  <xsl:template match="pc:PC-Compound_props">
    <xsl:variable name="infoData" select="pc:PC-InfoData" />
    <xsl:variable name="iupac" select="$infoData[pc:PC-InfoData_urn/pc:PC-Urn/pc:PC-Urn_label[.='IUPAC Name']]" />
    <xsl:for-each select="$iupac/pc:PC-InfoData_value/pc:PC-InfoData_value_sval">
      <cml:name dictRef="pc:iupac">
        <xsl:value-of select="." />
      </cml:name>
    </xsl:for-each>
  </xsl:template>


</xsl:stylesheet>