<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="2.0" cml:dummy-for-xmlns="" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:sch="http://www.ascc.net/xml/schematron" xmlns:iso="http://purl.oclc.org/dsdl/schematron" xmlns:cml="http://www.xml-cml.org/schema">

  <!--PHASES-->


  <!--PROLOG-->
  <xsl:output method="xml" omit-xml-declaration="no" standalone="yes" indent="yes" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

  <!--KEYS-->


  <!--DEFAULT RULES-->


  <!--MODE: SCHEMATRON-FULL-PATH-->
  <xsl:template match="*|@*" mode="schematron-get-full-path">
    <xsl:apply-templates select="parent::*" mode="schematron-get-full-path" />
    <xsl:text>/</xsl:text>
    <xsl:choose>
      <xsl:when test="namespace-uri()=''">
        <xsl:value-of select="name()" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>*[local-name()='</xsl:text>
        <xsl:value-of select="local-name()" />
        <xsl:text>
          ' and namespace-uri()='
        </xsl:text>
        <xsl:value-of select="namespace-uri()" />
        <xsl:text>']</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:variable name="preceding" select="count(preceding-sibling::*[local-name()=local-name(current())                                   and namespace-uri() = namespace-uri(current())])" />
    <xsl:text>[</xsl:text>
    <xsl:value-of select="1+ $preceding" />
    <xsl:text>]</xsl:text>
  </xsl:template>
  <xsl:template match="@*" mode="schematron-get-full-path">
    <xsl:apply-templates select="parent::*" mode="schematron-get-full-path" />
    @*[local-name()='schema' and namespace-uri()='http://purl.oclc.org/dsdl/schematron']
  </xsl:template>

  <!--MODE: GENERATE-ID-FROM-PATH -->
  <xsl:template match="/" mode="generate-id-from-path" />
  <xsl:template match="text()" mode="generate-id-from-path">
    <xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
    <xsl:value-of select="concat('.text-', 1+count(preceding-sibling::text()), '-')" />
  </xsl:template>
  <xsl:template match="comment()" mode="generate-id-from-path">
    <xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
    <xsl:value-of select="concat('.comment-', 1+count(preceding-sibling::comment()), '-')" />
  </xsl:template>
  <xsl:template match="processing-instruction()" mode="generate-id-from-path">
    <xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
    <xsl:value-of select="concat('.processing-instruction-', 1+count(preceding-sibling::processing-instruction()), '-')" />
  </xsl:template>
  <xsl:template match="@*" mode="generate-id-from-path">
    <xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
    <xsl:value-of select="concat('.@', name())" />
  </xsl:template>
  <xsl:template match="*" mode="generate-id-from-path" priority="-0.5">
    <xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
    <xsl:text>.</xsl:text>
    <xsl:choose>
      <xsl:when test="count(. | ../namespace::*) = count(../namespace::*)">
        <xsl:value-of select="concat('.namespace::-',1+count(namespace::*),'-')" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="concat('.',name(),'-',1+count(preceding-sibling::*[name()=name(current())]),'-')" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!--Strip characters-->
  <xsl:template match="text()" priority="-1" />

  <!--SCHEMA METADATA-->
  <xsl:template match="/">
    <svrl:schematron-output title="CMLLite schematron file" schemaVersion="ISO19757-3" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
      <svrl:text>
        <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
          Checks that the elements and attributes being used conform to the structure and subset as agreed upon by MS and UCC.
        </h:p>
        <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
          <h:b>NOTE</h:b>
          : the CMLLite schema defines the allowed attributes on elements, and their allowed children, however it can not perform further validation such
          as;
          <h:ul>
            <h:li>
              the eldest cml:cml element MUST have version and convention specified
            </h:li>
            <h:li>
              the ids of all the atoms referenced in a bond (via atomRefs2) must be defined within that molecule
            </h:li>
          </h:ul>
        </h:p>
        <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
          This stylesheet does not attempt to perform any chemical validation (other than that which is absolutely necessary for the document to make
          consistent sense - such as the rule about atom ids above) this is performed later.
        </h:p>
      </svrl:text>
      <svrl:ns-prefix-in-attribute-values uri="http://www.xml-cml.org/schema" prefix="cml" />
      <svrl:ns-prefix-in-attribute-values uri="http://www.w3.org//1999/XSL/Transform" prefix="xsl" />
      <svrl:active-pattern>
        <xsl:attribute name="id">atom.checks</xsl:attribute>
        <xsl:attribute name="name">atom element checks</xsl:attribute>
        <svrl:title>atom element checks</svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            Describe what further limitations we have put on the atom element
          </h:p>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M10" />
      <svrl:active-pattern>
        <xsl:attribute name="id">atomArray.checks</xsl:attribute>
        <xsl:attribute name="name">
          atomArray element checks
        </xsl:attribute>
        <svrl:title>
          atomArray element checks
        </svrl:title>
        <svrl:text>
          <h:div xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            atomArray must be in either molecule or formula but could be enclosed in a cml element (perhaps for some bizarre grouping)
          </h:div>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M11" />
      <svrl:active-pattern>
        <xsl:attribute name="id">atomParity.checks</xsl:attribute>
        <xsl:attribute name="name">
          atomParity element checks
        </xsl:attribute>
        <svrl:title>
          atomParity element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M12" />
      <svrl:active-pattern>
        <xsl:attribute name="id">bond.checks</xsl:attribute>
        <xsl:attribute name="name">bond element checks</xsl:attribute>
        <svrl:title>bond element checks</svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            Describe what further limitations we have put on the bond element
          </h:p>
        </svrl:text>
        <svrl:title>
          bondArray element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M13" />
      <svrl:active-pattern>
        <xsl:attribute name="id">bondStereo.checks</xsl:attribute>
        <xsl:attribute name="name">
          bondStereo element checks
        </xsl:attribute>
        <svrl:title>
          bondStereo element checks
        </svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            Describe what further limitations we have put on the bondStereo element
          </h:p>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M14" />
      <svrl:active-pattern>
        <xsl:attribute name="id">scalar.checks</xsl:attribute>
        <xsl:attribute name="name">
          scalar element checks
        </xsl:attribute>
        <svrl:title>
          scalar element checks
        </svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            Describe what further limitations we have put on the cml element
          </h:p>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M15" />
      <svrl:active-pattern>
        <xsl:attribute name="id">label.checks</xsl:attribute>
        <xsl:attribute name="name">label element checks</xsl:attribute>
        <svrl:title>label element checks</svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            labels should have convention specified if at all possible
          </h:p>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M16" />
      <svrl:active-pattern>
        <xsl:attribute name="id">molecule.checks</xsl:attribute>
        <xsl:attribute name="name">
          molecule element checks
        </xsl:attribute>
        <svrl:title>
          molecule element checks
        </svrl:title>
        <svrl:text>
          <h:div xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            how unique should the ids of molecule be?
          </h:div>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M17" />
      <svrl:active-pattern>
        <xsl:attribute name="id">formula.checks</xsl:attribute>
        <xsl:attribute name="name">
          formula element checks
        </xsl:attribute>
        <svrl:title>
          formula element checks
        </svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
            Describe what further limitations we have put on the atom element
          </h:p>
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M18" />
      <svrl:active-pattern>
        <xsl:attribute name="id">peak.checks</xsl:attribute>
        <xsl:attribute name="name">peak element checks</xsl:attribute>
        <svrl:title>peak element checks</svrl:title>
        <svrl:text>
          <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
        </svrl:text>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M19" />
      <svrl:active-pattern>
        <xsl:attribute name="id">peakList.checks</xsl:attribute>
        <xsl:attribute name="name">
          peakList element checks
        </xsl:attribute>
        <svrl:title>
          peakList element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M20" />
      <svrl:active-pattern>
        <xsl:attribute name="id">peakStructure.checks</xsl:attribute>
        <xsl:attribute name="name">
          peakStructure element checks
        </xsl:attribute>
        <svrl:title>
          peakStructure element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M21" />
      <svrl:active-pattern>
        <xsl:attribute name="id">property.checks</xsl:attribute>
        <xsl:attribute name="name">
          property element checks
        </xsl:attribute>
        <svrl:title>
          property element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M22" />
      <svrl:active-pattern>
        <xsl:attribute name="id">spectrum.checks</xsl:attribute>
        <xsl:attribute name="name">
          spectrum element checks
        </xsl:attribute>
        <svrl:title>
          spectrum element checks
        </svrl:title>
        <xsl:apply-templates />
      </svrl:active-pattern>
      <xsl:apply-templates select="/" mode="M23" />
    </svrl:schematron-output>
  </xsl:template>

  <!--SCHEMATRON PATTERNS-->
  <dc:title xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <dc:author xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <dc:contributor xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <dc:contributor xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <dc:rights xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <dc:description xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:h="http://www.w3.org/1999/xhtml" xmlns:fn="http://www.w3.org/2005/02/xpath-functions" />
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    CMLLite schematron file
  </svrl:title>

  <!--PATTERN atom.checksatom element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">atom element checks</svrl:title>

  <!--RULE -->
  <xsl:template match="cml:atom" priority="3998" mode="M10">
    <svrl:fired-rule context="cml:atom" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:atomArray" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:atomArray" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atom must be in an atomArray
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@id" />
      <xsl:otherwise>
        <svrl:failed-assert test="@id" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>atom must have an id</svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@elementType" />
      <xsl:otherwise>
        <svrl:failed-assert test="@elementType" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atom must have elementType
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(ancestor::cml:molecule//cml:atom[@id = current()/@id]) = 1" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(ancestor::cml:molecule//cml:atom[@id = current()/@id]) = 1" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the id of a atom must be unique within the eldest containing molecule (duplicate found:
            <xsl:text />
            <xsl:value-of select="@id" />
            <xsl:text />
            )
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@x2) or (@x2 and @y2)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@x2) or (@x2 and @y2)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if atom has @x2 then it must have @y2
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@y2) or (@x2 and @y2)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@y2) or (@x2 and @y2)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if atom has @y2 then it must have @x2
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@x3) or (@x3 and @y3 and @z3)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@x3) or (@x3 and @y3 and @z3)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if atom has @x3 then it must have @y3 and @z3
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@y3) or (@x3 and @y3 and @z3)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@y3) or (@x3 and @y3 and @z3)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if atom has @32 then it must have @x3 and @z3
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@z3) or (@x3 and @y3 and @z3)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@z3) or (@x3 and @y3 and @z3)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if atom has @z3 then it must have @x3 and @y3
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(child::cml:*) or (child::cml:label or cml:atomParity or cml:name)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(child::cml:*) or (child::cml:label or cml:atomParity or cml:name)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            If atom has a child element from the CML namespace it can only be label, atomParity or name
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M10" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M10" />
  <xsl:template match="@*|node()" priority="-2" mode="M10">
    <xsl:apply-templates select="@*|node()" mode="M10" />
  </xsl:template>

  <!--PATTERN atomArray.checksatomArray element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    atomArray element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:atomArray" priority="3998" mode="M11">
    <svrl:fired-rule context="cml:atomArray" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:molecule or parent::cml:formula" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:molecule or parent::cml:formula" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atomArray must be found in either a molecule or a formula
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(.//cml:atom) &gt; 0" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(.//cml:atom) &gt; 0" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            an atomArray must contain atoms
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M11" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M11" />
  <xsl:template match="@*|node()" priority="-2" mode="M11">
    <xsl:apply-templates select="@*|node()" mode="M11" />
  </xsl:template>

  <!--PATTERN atomParity.checksatomParity element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    atomParity element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:atomParity" priority="3999" mode="M12">
    <svrl:fired-rule context="cml:atomParity" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:atom" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:atom" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atomParity must be a direct child of atom
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@atomRefs4" />
      <xsl:otherwise>
        <svrl:failed-assert test="@atomRefs4" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            an atomParity must have atomRefs4
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M12" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M12" />
  <xsl:template match="@*|node()" priority="-2" mode="M12">
    <xsl:apply-templates select="@*|node()" mode="M12" />
  </xsl:template>

  <!--PATTERN bond.checksbond element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">bond element checks</svrl:title>

  <!--RULE -->
  <xsl:template match="cml:bond" priority="3998" mode="M13">
    <svrl:fired-rule context="cml:bond" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@id" />
      <xsl:otherwise>
        <svrl:failed-assert test="@id" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            a bond must have an id
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@atomRefs2" />
      <xsl:otherwise>
        <svrl:failed-assert test="@atomRefs2" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            bond must have atomRefs2
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:molecule[1]//cml:atom[@id = substring-before(current()/@atomRefs2, ' ')]" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:molecule[1]//cml:atom[@id = substring-before(current()/@atomRefs2, ' ')]" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the atoms in the atomRefs2 must be within the eldest containing molecule (not found [
            <xsl:text />
            <xsl:value-of select="substring-before(@atomRefs2, ' ')" />
            <xsl:text />
            ])
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:molecule[1]//cml:atom[@id = substring-after(current()/@atomRefs2, ' ')]" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:molecule[1]//cml:atom[@id = substring-after(current()/@atomRefs2, ' ')]" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the atoms in the atomRefs2 must be within the eldest containing molecule (not found [
            <xsl:text />
            <xsl:value-of select="substring-after(@atomRefs2, ' ')" />
            <xsl:text />
            ])
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(substring-before(@atomRefs2, ' ') = substring-after(@atomRefs2, ' '))" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(substring-before(@atomRefs2, ' ') = substring-after(@atomRefs2, ' '))" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            a bond must be between different atoms
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(ancestor::cml:molecule[1]//cml:bond[@id = current()/@id]) = 1" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(ancestor::cml:molecule[1]//cml:bond[@id = current()/@id]) = 1" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the id of a bond must be unique within the eldest containing molecule (duplicate found:
            <xsl:text />
            <xsl:value-of select="@id" />
            <xsl:text />
            )
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M13" />
  </xsl:template>

  <!--PATTERN bondArray.checksbondArray element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    bondArray element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:bondArray" priority="3999" mode="M3">
    <svrl:fired-rule context="cml:bondArray" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:molecule" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:molecule" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            bondArray must be found in a molecule
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(.//cml:bond) &gt; 0" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(.//cml:bond) &gt; 0" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            a bondArray must contain bonds
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M3" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M3" />
  <xsl:template match="@*|node()" priority="-2" mode="M3">
    <xsl:apply-templates select="@*|node()" mode="M3" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M13" />
  <xsl:template match="@*|node()" priority="-2" mode="M13">
    <xsl:apply-templates select="@*|node()" mode="M13" />
  </xsl:template>

  <!--PATTERN bondStereo.checksbondStereo element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    bondStereo element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:bondStereo" priority="3998" mode="M14">
    <svrl:fired-rule context="cml:bondStereo" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />
    <h:p xmlns:h="http://www.w3.org/1999/xhtml" xmlns="http://purl.oclc.org/dsdl/schematron" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:fn="http://www.w3.org/2005/02/xpath-functions">
      <h:div />
    </h:p>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            all valid CMLLite must be wrapped in a cml:cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:bond" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:bond" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            bondStereo element must be child of a bond
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@convention" />
      <xsl:otherwise>
        <svrl:failed-assert test="@convention" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            convention must be specified for bondStereo
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:variable name="cmlDict" select="name(namespace::*[.='http://www.xml-cml.org/dictionary/cml/'])" />
    <xsl:variable name="prefix" select="substring-before(@convention, ':')" />
    <xsl:variable name="value" select="substring-after(@convention, ':')" />

    <!--ASSERT 
    <xsl:choose>
      <xsl:when test="($cmlDict = $prefix and $value = 'wedgehatch') and . = 'W' or 'H'" />
      <xsl:otherwise>
        <svrl:failed-assert test="($cmlDict = $prefix and $value = 'wedgehatch') and . = 'W' or 'H'" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if  the convention is cml:wedgehatch then the content should be either W or H
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
-->
    <!--ASSERT 
    <xsl:choose>
      <xsl:when test="($cmlDict = $prefix and $value = 'cistrans') and . = 'C' or 'T'" />
      <xsl:otherwise>
        <svrl:failed-assert test="($cmlDict = $prefix and $value = 'cistrans') and . = 'C' or 'T'" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if the convention is cml:cistrans then the content should be either C or T
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
-->
    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="(. = 'W' or . = 'H') and @atomRefs4">
        <svrl:failed-assert test="(. = 'W' or . = 'H') and @atomRefs4" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atomRefs4 should not be present for wedge/hatch bondStereo
          </svrl:text>
        </svrl:failed-assert>
      </xsl:when>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="(. = 'C' or . = 'T') and not(@atomRefs4)">
        <svrl:failed-assert test="(. = 'C' or . = 'T') and not(@atomRefs4)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            atomRefs4 are required for cis/trans bondStereo (to define what is cis or trans to what)
          </svrl:text>
        </svrl:failed-assert>
      </xsl:when>
    </xsl:choose>

    <!--REPORT
    <xsl:if test="not($cmlDict = $prefix and $value = 'wedgehatch') and not($cmlDict = $prefix and $value = 'cistrans')">
      <svrl:successful-report test="not($cmlDict = $prefix and $value = 'wedgehatch') and not($cmlDict = $prefix and $value = 'cistrans')" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
        <xsl:attribute name="location">
          <xsl:apply-templates select="." mode="schematron-get-full-path" />
        </xsl:attribute>
        <svrl:text>only {http://www.xml-cml.org/dictionary/cml/}wedgehatch and {http://www.xml-cml.org/dictionary/cml/}cistrans bondStereo are currently supported</svrl:text>
      </svrl:successful-report>
    </xsl:if>
     -->
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M14" />

  </xsl:template>

  <xsl:template match="text()" priority="-1" mode="M14" />
  <xsl:template match="@*|node()" priority="-2" mode="M14">
    <xsl:apply-templates select="@*|node()" mode="M14" />
  </xsl:template>

  <!--PATTERN scalar.checksscalar element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    scalar element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:scalar" priority="3998" mode="M15">
    <svrl:fired-rule context="cml:scalar" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(@max | @min | text()) &gt;= 1" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(@max | @min | text()) &gt;= 1" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            scalar must have one or more max, min or content
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M15" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M15" />
  <xsl:template match="@*|node()" priority="-2" mode="M15">
    <xsl:apply-templates select="@*|node()" mode="M15" />
  </xsl:template>

  <!--PATTERN label.checkslabel element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">label element checks</svrl:title>

  <!--RULE -->
  <xsl:template match="cml:label" priority="3998" mode="M16">
    <svrl:fired-rule context="cml:label" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@dictRef" />
      <xsl:otherwise>
        <svrl:failed-assert test="@dictRef" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            label must have dictRef specified
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M16" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M16" />
  <xsl:template match="@*|node()" priority="-2" mode="M16">
    <xsl:apply-templates select="@*|node()" mode="M16" />
  </xsl:template>

  <!--PATTERN molecule.checksmolecule element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    molecule element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:cml//cml:molecule" priority="3998" mode="M17">
    <svrl:fired-rule context="cml:cml//cml:molecule" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@id" />
      <xsl:otherwise>
        <svrl:failed-assert test="@id" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            molecule must have an id
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(//cml:molecule[@id = current()/@id]) = 1" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(//cml:molecule[@id = current()/@id]) = 1" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the id of a molecule must be unique within the document (duplicate found:
            <xsl:text />
            <xsl:value-of select="@id" />
            <xsl:text />
            )
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            formula must be a descendent of a cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@count or not(ancestor::cml:molecule)" />
      <xsl:otherwise>
        <svrl:failed-assert test="@count or not(ancestor::cml:molecule)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            molecule children of molecule require a count
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M17" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M17" />
  <xsl:template match="@*|node()" priority="-2" mode="M17">
    <xsl:apply-templates select="@*|node()" mode="M17" />
  </xsl:template>

  <!--PATTERN formula.checksformula element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    formula element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:formula" priority="3998" mode="M18">
    <svrl:fired-rule context="cml:formula" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@count or not(ancestor::cml:formula)" />
      <xsl:otherwise>
        <svrl:failed-assert test="@count or not(ancestor::cml:formula)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            formula children of formula require a count
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="ancestor::cml:cml" />
      <xsl:otherwise>
        <svrl:failed-assert test="ancestor::cml:cml" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            formula must be a descendent of a cml element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--REPORT -->
    <xsl:if test="not(@concise)">
      <svrl:successful-report test="not(@concise)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
        <xsl:attribute name="location">
          <xsl:apply-templates select="." mode="schematron-get-full-path" />
        </xsl:attribute>
        <svrl:text>
          a formula should have @concise if at all possible
        </svrl:text>
      </svrl:successful-report>
    </xsl:if>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M18" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M18" />
  <xsl:template match="@*|node()" priority="-2" mode="M18">
    <xsl:apply-templates select="@*|node()" mode="M18" />
  </xsl:template>

  <!--PATTERN peak.checkspeak element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">peak element checks</svrl:title>

  <!--RULE -->
  <xsl:template match="cml:peak" priority="3998" mode="M19">
    <svrl:fired-rule context="cml:peak" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:peakList" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:peakList" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peak must be child of peakList
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@yValue and count(ancestor::cml:peakList[1]//cml:peak/@yValue) = count(ancestor::cml:peakList[1]//cml:peak)" />
      <xsl:otherwise>
        <svrl:failed-assert test="@yValue and count(ancestor::cml:peakList[1]//cml:peak/@yValue) = count(ancestor::cml:peakList[1]//cml:peak)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if peak has yValue then all peaks should have yValue
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@xValue or (xMax and xMin)" />
      <xsl:otherwise>
        <svrl:failed-assert test="@xValue or (xMax and xMin)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            the peak must have xValue and/or (xMax and xMin)
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@xMax) or (@xMax and @xMin)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@xMax) or (@xMax and @xMin)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peak must not have an isolated xMax attribute
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="not(@xMin) or (@xMax and @xMin)" />
      <xsl:otherwise>
        <svrl:failed-assert test="not(@xMin) or (@xMax and @xMin)" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peak must not have an isolated xMin attribute
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M19" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M19" />
  <xsl:template match="@*|node()" priority="-2" mode="M19">
    <xsl:apply-templates select="@*|node()" mode="M19" />
  </xsl:template>

  <!--PATTERN peakList.checkspeakList element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    peakList element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:peakList" priority="3999" mode="M20">
    <svrl:fired-rule context="cml:peakList" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@xUnits" />
      <xsl:otherwise>
        <svrl:failed-assert test="@xUnits" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>peakList</svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(.//cml:peak) &gt; 0" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(.//cml:peak) &gt; 0" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peakList must contain at least one peak element
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="count(.//cml:peak[@yValue]) &gt; 0 and @yUnits" />
      <xsl:otherwise>
        <svrl:failed-assert test="count(.//cml:peak[@yValue]) &gt; 0 and @yUnits" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            if peaks have y values then peakList must specify yUnits
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M20" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M20" />
  <xsl:template match="@*|node()" priority="-2" mode="M20">
    <xsl:apply-templates select="@*|node()" mode="M20" />
  </xsl:template>

  <!--PATTERN peakStructure.checkspeakStructure element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    peakStructure element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:peakStructure" priority="3999" mode="M21">
    <svrl:fired-rule context="cml:peakStructure" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="parent::cml:peak" />
      <xsl:otherwise>
        <svrl:failed-assert test="parent::cml:peak" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peakStructure must be a child of a peak
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@dictRef" />
      <xsl:otherwise>
        <svrl:failed-assert test="@dictRef" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            peakStructure must have a dictRef to explain what kind of peakStructure is being talked about
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M21" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M21" />
  <xsl:template match="@*|node()" priority="-2" mode="M21">
    <xsl:apply-templates select="@*|node()" mode="M21" />
  </xsl:template>

  <!--PATTERN property.checksproperty element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    property element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:property" priority="3999" mode="M22">
    <svrl:fired-rule context="cml:property" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@dictRef" />
      <xsl:otherwise>
        <svrl:failed-assert test="@dictRef" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            a dictRef is mandatory on property
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M22" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M22" />
  <xsl:template match="@*|node()" priority="-2" mode="M22">
    <xsl:apply-templates select="@*|node()" mode="M22" />
  </xsl:template>

  <!--PATTERN spectrum.checksspectrum element checks-->
  <svrl:title xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
    spectrum element checks
  </svrl:title>

  <!--RULE -->
  <xsl:template match="cml:spectrum" priority="3999" mode="M23">
    <svrl:fired-rule context="cml:spectrum" xmlns:svrl="http://purl.oclc.org/dsdl/svrl" />

    <!--ASSERT -->
    <xsl:choose>
      <xsl:when test="@dictRef" />
      <xsl:otherwise>
        <svrl:failed-assert test="@dictRef" xmlns:svrl="http://purl.oclc.org/dsdl/svrl">
          <xsl:attribute name="location">
            <xsl:apply-templates select="." mode="schematron-get-full-path" />
          </xsl:attribute>
          <svrl:text>
            a dictRef is mandatory on spectrum
          </svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:apply-templates select="@*|*|comment()|processing-instruction()" mode="M23" />
  </xsl:template>
  <xsl:template match="text()" priority="-1" mode="M23" />
  <xsl:template match="@*|node()" priority="-2" mode="M23">
    <xsl:apply-templates select="@*|node()" mode="M23" />
  </xsl:template>
</xsl:stylesheet>