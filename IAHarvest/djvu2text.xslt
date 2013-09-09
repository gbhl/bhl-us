<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'
	xmlns:dc='http://purl.org/dc/elements/1.1/'
	xmlns:dli="http://njlegallib.rutgers.edu/ns/1.0"
	xmlns="http://www.w3.org/1999/xhtml"
	version="1.0"
	exclude-result-prefixes="xsl rdf dc dli">

<!-- output declarations -->
<xsl:output method="text" omit-xml-declaration="yes" encoding="UTF-8"/>
<xsl:preserve-space elements="*"/>

<!-- root template -->
<xsl:template match="OBJECT">
	<xsl:apply-templates select="HIDDENTEXT/PAGECOLUMN"/>
</xsl:template>

<xsl:template match="HIDDENTEXT/PAGECOLUMN">
	<xsl:apply-templates select="REGION"/>
</xsl:template>

<xsl:template match="REGION">
	<xsl:apply-templates select="PARAGRAPH"/>
</xsl:template>

<xsl:template match="PARAGRAPH">
	<xsl:apply-templates select="LINE"/>&#013;
</xsl:template>

<xsl:template match="LINE">
	<xsl:if test="position() != 1"><xsl:text>&#032;</xsl:text></xsl:if>
	<xsl:apply-templates select="WORD"/>&#013;
</xsl:template>

<xsl:template match="WORD"><xsl:value-of select="."/>&#032;</xsl:template>

</xsl:stylesheet>
